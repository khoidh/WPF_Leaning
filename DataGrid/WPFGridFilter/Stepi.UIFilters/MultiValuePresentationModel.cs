using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using Stepi.Collections.Filters;
using System.ComponentModel;
using Stepi.Collections;
using System.Reflection;

namespace Stepi.UIFilters
{
    public class MultiValuePresentationModel<T> : IMultiValuePresentationModel
    {
        private static readonly PropertyChangedEventArgs _activeEventArgs = new PropertyChangedEventArgs("IsActive");

        private bool _isActive;
        private readonly string _name;
        private readonly ObservableCollection<UIFilterValueItem<T>> _availableValues = new ObservableCollection<UIFilterValueItem<T>>();
        private readonly IMultiValueFilter<T> _filter;
        private readonly ICollectionView _collection;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public MultiValuePresentationModel(IMultiValueFilter<T> filter, string name, ICollectionView collection)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (collection == null)
            {
                throw new ArgumentNullException("filteredCollection");
            }
            _filter = filter;
            _collection = collection;
            _name = name;
            _availableValues.CollectionChanged += new NotifyCollectionChangedEventHandler(OnAvailableValuesCollectionChanged);
        }

        /// <summary>
        /// Gets the name(description) of the filter
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this filter is active.
        /// </summary>
        /// <value><c>true</c> if this filter is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    PropertyChanged(this, _activeEventArgs);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the filter can be applied
        /// </summary>
        /// <value><c>true</c> if this instance can apply; otherwise, <c>false</c>.</value>
        public bool CanApply
        {
            get { return true; }
            set { }
        }

        /// <summary>
        /// Gets the available values.
        /// </summary>
        /// <value>The available values.</value>
        public IList<UIFilterValueItem<T>> AvailableValues
        {
            get { return _availableValues; }
        }

        /// <summary>
        /// Gets the available values.
        /// </summary>
        /// <value>The available values.</value>
        IEnumerable IMultiValuePresentationModel.AvailableValues
        {
            get { return _availableValues; }
        }

        public  IFilter Filter
        {
            get { return _filter; }
        }

        /// <summary>
        /// Gives the UI the option to update its data.
        /// </summary>
        /// <param name="isDisplaying">if set to <c>true</c> [is displaying].</param>
        public void UpdateData(bool isDisplaying)
        {
            if (isDisplaying)
            {
                _availableValues.Clear();
                UpdateAvailableValues();
            }
        }

        /// <summary>
        /// Updates the available values.
        /// </summary>
        private void UpdateAvailableValues()
        {
            PropertyInfo propInfo = Filter.PropertyInfo;
            Dictionary<T, bool> localValues = new Dictionary<T, bool>();
            foreach (object element in _collection.SourceCollection)
            {
                try
                {
                    T value = (T)propInfo.GetValue(element, null);
                    if (!localValues.ContainsKey(value))
                    {
                        localValues[value] = true;
                        UIFilterValueItem<T> item = new UIFilterValueItem<T>(value);
                        if (_filter.Values.Contains(value))
                        {
                            item.IsSelected = true;
                        }
                        _availableValues.Add(item);
                    }
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Called when [available values collection changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnAvailableValuesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (UIFilterValueItem<T> value in e.NewItems)
                {
                    //AddValue(value);
                    value.PropertyChanged+=new PropertyChangedEventHandler(OnUIElementIsSelectedPropertyChanged);
                    if (value.IsSelected == true)
                    {
                        AddValue(value);
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (UIFilterValueItem<T> value in e.OldItems)
                {
                    //value.SelectedChanged -= new EventHandler(OnFilterValueSelectedChanged);
                    value.PropertyChanged -= new PropertyChangedEventHandler(OnUIElementIsSelectedPropertyChanged);
                    RemoveValue(value);
                }
            }
        }

        /// <summary>
        /// Called when [UI element is selected property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnUIElementIsSelectedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UIFilterValueItem<T> value = (UIFilterValueItem<T>)sender;
            if (value.IsSelected == true)
            {
                AddValue(value);
            }
            else 
            {
                RemoveValue(value);
            }
        }

        /// <summary>
        /// Removes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        private void RemoveValue(UIFilterValueItem<T> value)
        {
            if (value.IsSelected == false)
            {
                _filter.Values.Remove(value.Value);
            }
        }

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="value">The value.</param>
        private void AddValue(UIFilterValueItem<T> value)
        {
            if (value.IsSelected == true && !_filter.Values.Contains(value.Value))
            {
                _filter.Values.Add(value.Value);
            }
        }

        /// <summary>
        /// Called when [filter value selected changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnFilterValueSelectedChanged(object sender, EventArgs e)
        {
            UIFilterValueItem<T> value = (UIFilterValueItem<T>)sender;
            AddValue(value);
        }
    }
}
