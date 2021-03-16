using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Stepi.Collections;

namespace Stepi.UIFilters
{
    public class FiltersViewPresentationModel
    {
        private PropertyInfo _propertyInfo;

        private bool _isDisplaying;

        private IFilteredCollection _filteredCollection;

        private IList<IFilterView> _views;

        private IUIFilterPresentationModel _previousActive = null;

        public FiltersViewPresentationModel(PropertyInfo propertyInfo, IFilteredCollection filteredCollection, IFilterViewInitializersManager initializer)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            if (filteredCollection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (initializer == null)
            {
                throw new ArgumentNullException("initializer");
            }
            Debug.Assert(filteredCollection.GetType().IsGenericType, "Filtered collection needs to be a generic type");
            _propertyInfo = propertyInfo;
            _filteredCollection = filteredCollection;
            InitilizeViewsCollection(propertyInfo, initializer);
        }

        //public FiltersViewPresentationModel(string propertyName, IFilteredCollection filteredCollection, IFilterViewInitializersManager initializer)
        //{
        //    if (string.IsNullOrEmpty(propertyName))
        //    {
        //        throw new ArgumentException("Invalid property name specified", "propertyName");
        //    }
        //    if (initializer == null)
        //    {
        //        throw new ArgumentNullException("initializer");
        //    }
        //    if (filteredCollection == null)
        //    {
        //        throw new ArgumentNullException("filteredCollection");
        //    }
        //    if (!filteredCollection.GetType().IsGenericType)
        //    {
        //        throw new ArgumentException("Invalid filtered collection specified. Needs to be generic type");
        //    }
        //    Type[] genericTypes = filteredCollection.GetType().GetGenericArguments();
            
        //}

        private void InitilizeViewsCollection(PropertyInfo propertyInfo, IFilterViewInitializersManager initializer)
        {
            _views = new List<IFilterView>();
            foreach(IFilterView view in initializer.CreateView(propertyInfo, _filteredCollection))
            {
                //TODO: refactor this not to be dependend on the model
                Debug.Assert(view.Model != null, "Filter view presentation model is null");
                view.Model.PropertyChanged += new PropertyChangedEventHandler(OnFilterViewModelPropertyChanged);
                _views.Add(view);
            }
        }

        private void OnFilterViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IUIFilterPresentationModel presentationModel = (IUIFilterPresentationModel)sender;
            if (presentationModel.IsActive)
            {
                if (presentationModel == _previousActive)
                {
                    return;
                }
                if (_previousActive != null)
                {
                    _previousActive.IsActive = false;
                    //no need for this one as it will raise the event and will get to the second else statement
                    //_filteredCollection.RemoveFilter(_previousActive.Filter);
                }
                _previousActive = presentationModel;
                _filteredCollection.AddFilter(presentationModel.Filter);
            }
            else
            {
                if (presentationModel == _previousActive)
                {
                    _previousActive = null;
                    _filteredCollection.RemoveFilter(presentationModel.Filter);
                }
            }
        }

        public IEnumerable<IFilterView> Filters
        {
            get 
            {
                foreach (IFilterView view in _views)
                {
                    yield return view;
                }
            }
        }

        public bool IsDisplaying
        {
            get
            {
                return _isDisplaying;
            }
            set
            {
                if (_isDisplaying != value)
                {
                    _isDisplaying = value;
                    foreach (IFilterView view in _views)
                    {
                        view.Model.UpdateData(value);
                    }
                }
            }
        }
    }
}
