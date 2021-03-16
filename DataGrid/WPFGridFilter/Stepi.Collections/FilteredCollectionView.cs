
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using Wintellect.PowerCollections;
using System.Diagnostics;


namespace Stepi.Collections
{
    /// <summary>
    /// View on top of a collection of INotifyPropertyChanged elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FilteredCollectionView<T> :  IFilteredCollection, INotifyPropertyChanged
        where T : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly CurrentChangingEventArgs _currentChangingEventArgs = new CurrentChangingEventArgs(false);

        /// <summary>
        /// Holds how many times deferrefresh has been called
        /// </summary>
        private int _defer;

        /// <summary>
        /// The index in the oredered set pointing to the current item
        /// </summary>
        private int _currentIndex = -1;

        /// <summary>
        /// Points to the current item in the collection view
        /// </summary>
        private object _currentItem;

        /// <summary>
        /// 
        /// </summary>
        private CultureInfo _cultureInfo;

        /// <summary>
        /// Instance of the registered filters
        /// </summary>
        private Dictionary<string, IFilter> _filters = new Dictionary<string, IFilter>();

        /// <summary>
        /// Instance of the source collection
        /// </summary>
        private System.Collections.Generic.IList<T> _collection;

        /// <summary>
        /// Links the elements in the ordered set with the equivalent in the source collection
        /// </summary>
        private Dictionary<T, CollectionItem> _linkDictionary = new Dictionary<T, CollectionItem>();

        /// <summary>
        /// Contains the list of elements
        /// </summary>
        private OrderedSet<CollectionItem> _localCollection;
        //private TreeBag<int> _localCollection;
        private bool _isRemoving;
        

        /// <summary>
        /// Instance of SortDescription elements
        /// </summary>
        private SortDescriptionCollection _sortDescription = new SortDescriptionCollection();

        /// <summary>
        /// Instance of the comparer used for sorting the itmes in the collection
        /// </summary>
        private FilteredSortComparer<T> _comparer;

        /// <summary>
        /// Occurs after the current item has been changed.
        /// </summary>
        public event EventHandler CurrentChanged = delegate { };

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Occurs when the items list of the collection has changed, or the collection is reset.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };

        /// <summary>
        /// Occurs before the current item changes.
        /// </summary>
        public event CurrentChangingEventHandler CurrentChanging = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredCollectionView&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public FilteredCollectionView(System.Collections.Generic.IList<T> collection)
        {
            if (null == collection)
            {
                throw new ArgumentNullException("collection");
            }
            _collection = collection;


            //handle the sortdescription collection change event (will cause the ordered set to be rebuild
            ((INotifyCollectionChanged)_sortDescription).CollectionChanged += (sender, e) =>
                                                                                        {
                                                                                            if (_sortDescription.Count > 0)
                                                                                            {
                                                                                                _comparer = new FilteredSortComparer<T>(_sortDescription);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                _comparer = null;
                                                                                            }
                                                                                            Refresh();
                                                                                        };

            //provide the comparison delegate
            //_localCollection = new TreeBag<int>(new LocalCollectionComparer<T>(this));
            _localCollection = new OrderedSet<CollectionItem>(new LocalCollectionComparer<T>(this));

            BuildLocalCollection(true);
            INotifyCollectionChanged collectionChanged = collection as INotifyCollectionChanged;
            if (collectionChanged != null)
            {
                collectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCollectionChanged);
            }
        }

        /// <summary>
        /// Returns the index of the given element in the ordered set.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public virtual int IndexOf(T item)
        {
            if (_linkDictionary.ContainsKey(item))
            {
                return _localCollection.IndexOf(_linkDictionary[item]);
            }
            return -1;
        }

        /// <summary>
        /// Builds the collection of the elements passing the registered filters
        /// ///
        /// </summary>
        /// <param name="hookEvent">if set to <c>true</c> [hook event].</param>
        private void BuildLocalCollection(bool hookEvent)
        {
            _localCollection.Clear();
            _linkDictionary.Clear();

            for (int i = 0; i < _collection.Count; ++i)
            {
                T item = _collection[i];
                if (PassesFilter(item))
                {
                    CollectionItem localItem = new CollectionItem(i);
                    _localCollection.Add(localItem);
                    _linkDictionary[item] = localItem;// index;
                }
                if (hookEvent)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(OnItemPropertyChanged);
                }
            }

            for (int i = 0; i < _localCollection.Count; ++i)
            {
                _localCollection[i].OrderedIndex = i;
            }
        }

        /// <summary>
        /// Sets the current item and position in the collection view.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <param name="newPosition">The new position.</param>
        private void SetCurrent(object newItem, int newPosition)
        {
            _currentItem = newItem;
            _currentIndex = newPosition;
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private bool RemoveItem(T item)
        {
            CollectionItem localItem;
            if (_linkDictionary.TryGetValue(item, out localItem))
            {
                int index =  InternalRemove(localItem);
                _linkDictionary.Remove(item);

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));

                return true;
            }

            return false;
        }

        private int InternalRemove(CollectionItem localItem)
        {
            _isRemoving = true;
            _localCollection.Remove(localItem);
            for (int i = localItem.OrderedIndex; i < _localCollection.Count; ++i)
            {
                _localCollection[i].OrderedIndex--;
            }
            _isRemoving = false;
            return localItem.OrderedIndex;
        }

        /// <summary>
        /// Adds the item to the ordered set. is called once the source collection notifies there is an update
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private bool AddItem(T item, bool checkFilter, int sourceIndex)
        {
            Debug.Assert(sourceIndex >= 0 && sourceIndex < _collection.Count, "Invalid index to source collection");
            if (checkFilter && !PassesFilter(item))
            {
                return false;
            }

            Debug.Assert(_linkDictionary.ContainsKey(item) == false, " Item should not be present in the link collection");

            CollectionItem localItem = new CollectionItem(sourceIndex);
            _localCollection.Add(localItem);
            localItem.OrderedIndex = _localCollection.IndexOf(localItem);
            for (int i = localItem.OrderedIndex + 1; i < _localCollection.Count; ++i)
            {
                _localCollection[i].OrderedIndex = i;
            }
            _linkDictionary[item] = localItem;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, localItem.OrderedIndex));

            return true;
        }


        /// <summary>
        /// Called whenever one of the properties of an item in the source collection changes
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            T item = (T)sender;
            if (_filters.ContainsKey(e.PropertyName))
            {
                int sourceIndex = GetSourceCollectionIndex(item);
                RemoveItem(item);
                //check to see if it should be in the view
                if (PassesFilter(item))
                {
                    AddItem(item, false, sourceIndex);
                    UpdateCurrentItemPosition(item);
                }
                else
                {
                    //check if the current item was item
                    UpdateCurrentToPosition(_currentIndex);
                }
            }
            else
            {
                if (_sortDescription.Count > 0)
                {
                    if(IsAffectingSorting(e.PropertyName))
                    {
                        int sourceIndex = GetSourceCollectionIndex(item);

                        RemoveItem(item);
                        AddItem(item, false, sourceIndex);
                        UpdateCurrentItemPosition(item);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the current position if the item being update is the current one
        /// </summary>
        /// <param name="item">The item.</param>
        private void UpdateCurrentItemPosition(T item)
        {
            T currentItem = (T)CurrentItem;
            if (item.Equals(currentItem))
            {
                CollectionItem localItem = _linkDictionary[item];
                if (_currentIndex != localItem.OrderedIndex)
                {
                    SetCurrent(_currentItem, localItem.OrderedIndex);
                    OnCurrentChanged();
                    OnPropertyChanged("CurrentPosition");
                }
            }
        }

        /// <summary>
        /// Determines whether if the specified property name affects sorting
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// 	<c>true</c> if [is affecting sorting] [the specified property name]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAffectingSorting(string propertyName)
        {
            bool found = false;
            foreach (SortDescription sortDescription in _sortDescription)
            {
                if (_sortDescription[0].PropertyName == propertyName)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private int GetSourceCollectionIndex(T item)
        {
            int sourceIndex = -1;
            CollectionItem localItem;
            if (_linkDictionary.TryGetValue(item, out localItem))
            {
                sourceIndex = localItem.OriginalIndex;
            }
            else
            {
                sourceIndex = _collection.IndexOf(item);
            }
            return sourceIndex;
        }


        /// <summary>
        /// Handles the collection changed event on the source collection. The local ordered set is being updated
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    int startIndex = e.NewStartingIndex;
                    foreach (T item in e.NewItems)
                    {
                        AddItem(item, true, startIndex++);
                        item.PropertyChanged += new PropertyChangedEventHandler(OnItemPropertyChanged);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                    {
                        RemoveItem(item);
                        item.PropertyChanged -= new PropertyChangedEventHandler(OnItemPropertyChanged);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (T item in e.OldItems)
                    {
                        RemoveItem(item);
                        item.PropertyChanged -= new PropertyChangedEventHandler(OnItemPropertyChanged);
                    }
                    int index = e.NewStartingIndex;
                    foreach (T item in e.NewItems)
                    {
                        AddItem(item, true, index++);
                        item.PropertyChanged += new PropertyChangedEventHandler(OnItemPropertyChanged);
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    //_localCollection.Clear();
                    //_linkDictionary.Clear();
                    //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    Refresh();
                    break;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:CollectionChanged"/> event for this collection
        /// </summary>
        /// <param name="args">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged(this, args);
            if ((args.Action != NotifyCollectionChangedAction.Replace))
            {
                OnPropertyChanged("Count");
            }
        }

        /// <summary>
        /// Called to raise the property changed event on the current instance
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks if the item is passing the registered filters.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public virtual bool PassesFilter(T item)
        {
            bool result = true;
            if (CanFilter && (_filters.Count > 0))
            {
                foreach (IFilter filter in _filters.Values)
                {
                    if (!filter.IsMatch(item))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        #region ICollectionView Members

        /// <summary>
        /// Gets a value that indicates whether this view supports filtering by way of the <see cref="P:System.ComponentModel.ICollectionView.Filter"/> property.
        /// </summary>
        /// <value></value>
        /// <returns>true if this view supports filtering; otherwise, false.
        /// </returns>
        public bool CanFilter
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether this view supports grouping by way of the <see cref="P:System.ComponentModel.ICollectionView.GroupDescriptions"/> property.
        /// </summary>
        /// <value></value>
        /// <returns>true if this view supports grouping; otherwise, false.
        /// </returns>
        public bool CanGroup
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether this view supports sorting by way of the <see cref="P:System.ComponentModel.ICollectionView.SortDescriptions"/> property.
        /// </summary>
        /// <value></value>
        /// <returns>true if this view supports sorting; otherwise, false.
        /// </returns>
        public bool CanSort
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Indicates whether the specified item belongs to this collection view.
        /// </summary>
        /// <param name="item">The object to check.</param>
        /// <returns>
        /// true if the item belongs to this collection view; otherwise, false.
        /// </returns>
        public bool Contains(object item)
        {
            if (!(item is T))
            {
                return false;
            }
            return _linkDictionary.ContainsKey((T)item);
        }

        /// <summary>
        /// Gets or sets the cultural information for any operations of the view that may differ by culture, such as sorting.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The culture information to use during culture-sensitive operations.
        /// </returns>
        public CultureInfo Culture
        {
            get
            {
                return _cultureInfo;
            }
            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("Culture");
                }
                if (value != _cultureInfo)
                {
                    _cultureInfo = value;
                    OnPropertyChanged("Culture");
                }
            }
        }

        /// <summary>
        /// Called when [current changed].
        /// </summary>
        protected virtual void OnCurrentChanged()
        {
            CurrentChanged(this, EventArgs.Empty);
        }


        /// <summary>
        /// Raises the <see cref="E:CurrentChanging"/> event.
        /// </summary>
        /// <param name="args">The <see cref="System.ComponentModel.CurrentChangingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCurrentChanging(CurrentChangingEventArgs args)
        {
            CurrentChanging(this, args);
        }


        /// <summary>
        /// Gets the current item in the view.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The current item in the view or null if there is no current item.
        /// </returns>
        public object CurrentItem
        {
            get
            {
                return _currentItem;
            }
        }

        /// <summary>
        /// Gets the ordinal position of the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> in the view.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The ordinal position of the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> in the view.
        /// </returns>
        public int CurrentPosition
        {
            get
            {
                return _currentIndex;
            }
        }

        /// <summary>
        /// Enters a defer cycle that you can use to merge changes to the view and delay automatic refresh.
        /// </summary>
        /// <returns>
        /// The typical usage is to create a using scope with an implementation of this method and then include multiple view-changing calls within the scope. The implementation should delay automatic refresh until after the using scope exits.
        /// </returns>
        public IDisposable DeferRefresh()
        {
            _defer++;
            return new DeferRefreshHelper(this);
        }

        /// <summary>
        /// Ends the defer.
        /// </summary>
        private void OnFinishDefer()
        {
            _defer--;
            if (_defer == 0)
            {
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a callback that is used to determine whether an item is appropriate for inclusion in the view.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A method that is used to determine whether an item is appropriate for inclusion in the view.
        /// </returns>
        Predicate<object> ICollectionView.Filter
        {
            get
            {
                //throw new NotSupportedException();
                return null;
            }
            set
            {
                //throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="T:System.ComponentModel.GroupDescription"/> objects that describe how the items in the collection are grouped in the view.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A collection of objects that describe how the items in the collection are grouped in the view.
        /// </returns>
        public virtual ObservableCollection<GroupDescription> GroupDescriptions
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the top-level groups.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A read-only collection of the top-level groups or null if there are no groups.
        /// </returns>
        public ReadOnlyObservableCollection<object> Groups
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                return _localCollection.Count;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> of the view is beyond the end of the collection.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> of the view is beyond the end of the collection; otherwise, false.
        /// </returns>
        public bool IsCurrentAfterLast
        {
            get
            {
                return _currentIndex >= Count;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> of the view is beyond the start of the collection.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> of the view is beyond the start of the collection; otherwise, false.
        /// </returns>
        public bool IsCurrentBeforeFirst
        {
            get
            {
                if (!IsEmpty)
                {
                    return (_currentIndex < 0);
                }
                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the view is empty.
        /// </summary>
        /// <value></value>
        /// <returns>true if the view is empty; otherwise, false.
        /// </returns>
        public bool IsEmpty
        {
            get
            {

                return _localCollection.Count == 0;
            }
        }

        /// <summary>
        /// Moves the current to position internal.
        /// </summary>
        /// <param name="index">The index.</param>
        private void SetCurrentToPosition(int index)
        {
            if (index < 0)
            {
                SetCurrent(null, -1);
            }
            else if (index >= Count)
            {
                SetCurrent(null, Count);
            }
            else
            {
                int originalIndex = _localCollection[index].OriginalIndex;
                SetCurrent(_collection[originalIndex], index);
            }
        }

        /// <summary>
        /// Sets the specified item in the view as the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/>.
        /// </summary>
        /// <param name="item">The item to set as the current item.</param>
        /// <returns>
        /// true if the resulting <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> is an item in the view; otherwise, false.
        /// </returns>
        public bool MoveCurrentTo(object item)
        {
            T element = (T)item;
            if (object.Equals(CurrentItem, item) && ((item != null) || IsCurrentInView))
            {
                return IsCurrentInView;
            }
            int position = -1;
            if (PassesFilter(element))
            {
                //AddItem(element, false, _linkDictionary[element].OriginalIndex);
                position = IndexOf(element);
            }
            else
            {
                RemoveItem(element);
            }
            return MoveCurrentToPosition(position);
        }

        /// <summary>
        /// Sets the first item in the view as the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/>.
        /// </summary>
        /// <returns>
        /// true if the resulting <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> is an item in the view; otherwise, false.
        /// </returns>
        public bool MoveCurrentToFirst()
        {
            return MoveCurrentToPosition(0);
        }

        /// <summary>
        /// Sets the last item in the view as the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/>.
        /// </summary>
        /// <returns>
        /// true if the resulting <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> is an item in the view; otherwise, false.
        /// </returns>
        public bool MoveCurrentToLast()
        {
            return MoveCurrentToPosition(Count - 1);
        }

        /// <summary>
        /// Sets the item after the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> in the view as the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/>.
        /// </summary>
        /// <returns>
        /// true if the resulting <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> is an item in the view; otherwise, false.
        /// </returns>
        public bool MoveCurrentToNext()
        {
            return ((CurrentPosition < Count) && MoveCurrentToPosition(CurrentPosition + 1));
        }

        /// <summary>
        /// Sets the item at the specified index to be the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> in the view.
        /// </summary>
        /// <param name="position">The index to set the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> to.</param>
        /// <returns>
        /// true if the resulting <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> is an item in the view; otherwise, false.
        /// </returns>
        public bool MoveCurrentToPosition(int position)
        {
            if ((position < -1) || (position > Count))
            {
                throw new ArgumentOutOfRangeException("position");
            }
            if (AllowCurrentChange())
            {
                if ((position != CurrentPosition) || !IsCurrentInSync)
                {
                    UpdateCurrentToPosition(position);
                }
            }
            return IsCurrentInView;
        }

        /// <summary>
        /// Updates the current item and position to the given index
        /// </summary>
        /// <param name="position">The new position.</param>
        private void UpdateCurrentToPosition(int position)
        {
            bool isCurrentAfterLast = IsCurrentAfterLast;
            bool isCurrentBeforeFirst = IsCurrentBeforeFirst;
            SetCurrentToPosition(position);
            OnCurrentChanged();
            if (IsCurrentAfterLast != isCurrentAfterLast)
            {
                OnPropertyChanged("IsCurrentAfterLast");
            }
            if (IsCurrentBeforeFirst != isCurrentBeforeFirst)
            {
                OnPropertyChanged("IsCurrentBeforeFirst");
            }
            OnPropertyChanged("CurrentPosition");
            OnPropertyChanged("CurrentItem");
        }

        /// <summary>
        /// Sets the item before the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> in the view to the <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/>.
        /// </summary>
        /// <returns>
        /// true if the resulting <see cref="P:System.ComponentModel.ICollectionView.CurrentItem"/> is an item in the view; otherwise, false.
        /// </returns>
        public bool MoveCurrentToPrevious()
        {
            return ((CurrentPosition >= 0) && MoveCurrentToPosition(CurrentPosition - 1));
        }

        /// <summary>
        /// Recreates the view.
        /// </summary>
        public void Refresh()
        {
            //hold the existing values to be able to raise the appropriate property changed events
            bool oldIsCurrentAfterLast = IsCurrentAfterLast;
            bool oldIsCurrentBeforeFirst = IsCurrentBeforeFirst;
            object oldItem = _currentItem;
            int oldIndex = _currentIndex;

            BuildLocalCollection(false);
            OnCurrentChanging(_currentChangingEventArgs);
            if (IsEmpty || IsCurrentBeforeFirst)
            {
                SetCurrentToPosition(-1);
            }
            else if (IsCurrentAfterLast)
            {
                SetCurrentToPosition(Count);
            }
            else if (_currentItem != null)
            {
                T item = (T)_currentItem;
                int index = 0;
                if (_linkDictionary.ContainsKey(item))
                {
                    index = _localCollection.IndexOf(_linkDictionary[item]);
                }
                SetCurrentToPosition(index);
            }
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnCurrentChanged();
            
            if (oldIndex != _currentIndex)
            {
                OnPropertyChanged("CurrentPosition");
            }
            if (oldItem != _currentItem)
            {
                OnPropertyChanged("CurrentItem");
            }
            if (IsCurrentAfterLast != oldIsCurrentAfterLast)
            {
                OnPropertyChanged("IsCurrentAfterLast");
            }
            if (IsCurrentBeforeFirst != oldIsCurrentBeforeFirst)
            {
                OnPropertyChanged("IsCurrentBeforeFirst");
            }
            

        }

        /// <summary>
        /// Determines whether this instance [can current change].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance [can current change]; otherwise, <c>false</c>.
        /// </returns>
        private bool AllowCurrentChange()
        {
            CurrentChangingEventArgs args = new CurrentChangingEventArgs();
            OnCurrentChanging(args);
            return !args.Cancel;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is current in sync.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is current in sync; otherwise, <c>false</c>.
        /// </value>
        protected bool IsCurrentInSync
        {
            get
            {
                if (IsCurrentInView)
                {
                    return (((object)_collection[_localCollection[_currentIndex].OriginalIndex]) == _currentItem);
                }
                return (_currentItem == null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is current in view.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is current in view; otherwise, <c>false</c>.
        /// </value>
        private bool IsCurrentInView
        {
            get
            {
                return ((0 <= _currentIndex) && (_currentIndex < Count));
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="T:System.ComponentModel.SortDescription"/> instances that describe how the items in the collection are sorted in the view.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A collection of values that describe how the items in the collection are sorted in the view.
        /// </returns>
        public virtual SortDescriptionCollection SortDescriptions
        {
            get
            {
                return _sortDescription;
            }
        }

        public IEnumerable SourceCollection
        {
            get
            {
                return _collection;
            }
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            List<T> items = new List<T>();
            //IEnumerator<FilteredCollectionView<T>.CollectionItem> enumerator = _localCollection.GetEnumerator();
            foreach (CollectionItem item in _localCollection)
            //while(enumerator.MoveNext())
            {
                T t = _collection[item.OriginalIndex];
                items.Add(t);
            }
            return items.GetEnumerator();
        }

        #endregion

        #region Nested Class

        private class LocalCollectionComparer<T> : IComparer<CollectionItem>
          where T : INotifyPropertyChanged
        {
            private FilteredCollectionView<T> _filteredCollection;

            internal LocalCollectionComparer(FilteredCollectionView<T> filteredCollection)
            {
                if (filteredCollection == null)
                {
                    throw new ArgumentNullException("collection");
                }
                _filteredCollection = filteredCollection;
            }


            #region IComparer<int> Members

            public int Compare(CollectionItem x, CollectionItem y)
            {
                if (x.OrderedIndex != -1 && y.OrderedIndex != -1)
                {
                    return x.OrderedIndex.CompareTo(y.OrderedIndex);
                }
                if (!_filteredCollection._isRemoving && _filteredCollection._comparer != null)
                {
                    int compare =  _filteredCollection._comparer.Compare(_filteredCollection._collection[x.OriginalIndex], _filteredCollection._collection[y.OriginalIndex]);
                    if (compare == 0)
                    {
                        return x.OriginalIndex.CompareTo(y.OriginalIndex);
                    }
                    return compare;
                }
                return x.OriginalIndex.CompareTo(y.OriginalIndex);
            }

            #endregion
        }


        /// <summary>
        /// Simple IDisposable to handle postponing the collection refresh
        /// </summary>
        private class DeferRefreshHelper : IDisposable
        {
            private FilteredCollectionView<T> _collectionView;

            public DeferRefreshHelper(FilteredCollectionView<T> collectionView)
            {
                _collectionView = collectionView;
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (null != _collectionView)
                {
                    _collectionView.OnFinishDefer();
                    _collectionView = null;
                }
            }

            #endregion
        }


        private class CollectionItem
        {
            private readonly int _originalIndex;
            private int _orderedIndex = -1;

            public CollectionItem(int originalIndex)
            {
                _originalIndex = originalIndex;
            }

            public int OriginalIndex
            {
                get { return _originalIndex; }
            }

            public int OrderedIndex
            {
                get { return _orderedIndex; }
                set { _orderedIndex = value; }
            }
        }

        #endregion

        #region IFilteredCollection Members

        /// <summary>
        /// Registers a filter withe the collection view.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void AddFilter(IFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (filter.PropertyInfo == null)
            {
                throw new ArgumentException("Invalid filter, missing property info.");
            }
            if (!_filters.ContainsKey(filter.PropertyInfo.Name))
            {
                _filters[filter.PropertyInfo.Name] = filter;
                filter.FilteringChanged += new EventHandler(OnFilterChanged);
                Refresh();
            }
        }

        /// <summary>
        /// Called by the IFilter whenever it changes. Will cause a collection refresh.
        /// </summary>
        /// <param name="sender">The IFIlter registered with the collection.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnFilterChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Removes a filter from the collection.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void RemoveFilter(IFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (filter.PropertyInfo == null)
            {
                throw new ArgumentException("Invalid filter, missing property info.");
            }

            if (_filters.ContainsKey(filter.PropertyInfo.Name))
            {
                _filters.Remove(filter.PropertyInfo.Name);
                filter.FilteringChanged -= new EventHandler(OnFilterChanged);
                Refresh();
            }
        }

        /// <summary>
        /// Gets the type of the element in the collection
        /// </summary>
        /// <value>The type.</value>
        public Type Type
        {
            get { return typeof(T); }
        }

        #endregion
    }
}
