using System;
using Stepi.Collections;
using Stepi.Collections.Filters;
using System.ComponentModel;

namespace Stepi.UIFilters
{
    public class CompareFilterPresentationModel<T> : ICompareFilterPresentationModel<T>
        where T : IComparable
    {
        private static readonly PropertyChangedEventArgs _activeEventArgs = new PropertyChangedEventArgs("IsActive");
        private static readonly PropertyChangedEventArgs _applyEventArgs = new PropertyChangedEventArgs("CanApply");

        private bool _targetError;
        private bool _isActive;
        private bool _canApply = true;
        private readonly ICompareFilter<T> _filter;
        private readonly string _name;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public CompareFilterPresentationModel(ICompareFilter<T> compareFilter, string name)
        {
            if (compareFilter == null)
            {
                throw new ArgumentNullException("compareFilter");
            }
            _name = name;
            _filter = compareFilter;
        }

        public T Target
        {
            get
            {
                return _filter.CompareTo;
            }
            set
            {

                _filter.CompareTo = value;
            }
        }

        public string Name
        {
            get { return _name; }
        }

        object ICompareFilterPresentationModel.Target
        {
            get
            {
                return _filter.CompareTo;
            }
            set
            {
                _targetError = false;
                if (!(value is T))
                {
                    _targetError = true;
                    IsActive = false;
                    return;
                }

                _filter.CompareTo = (T)value;

            }
        }

        public IFilter Filter
        {
            get { return _filter; }
        }

        public bool CanApply
        {
            get { return _canApply; }
            set
            {
                if (_canApply != value)
                {
                    _canApply = value;
                    PropertyChanged(this, _applyEventArgs);
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    PropertyChanged(this, _activeEventArgs);
                }
            }
        }

        public void UpdateData(bool isDisplaying)
        { }

    }
}
