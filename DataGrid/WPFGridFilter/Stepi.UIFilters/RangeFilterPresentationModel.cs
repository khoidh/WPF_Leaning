using System;
using Stepi.Collections.Filters;
using Stepi.Collections;
using System.ComponentModel;

namespace Stepi.UIFilters
{
    public class RangeFilterPresentationModel<T> : IRangePresentationModel<T>
        where T : IComparable
    {
        private const string _name = "Range Filter";
        private static readonly PropertyChangedEventArgs _activeEventArgs = new PropertyChangedEventArgs("IsActive");
        private static readonly PropertyChangedEventArgs _applyEventArgs = new PropertyChangedEventArgs("CanApply");

        private bool _fromError;
        private bool _toError;
        private bool _isActive;
        private bool _canApply = true;
        private readonly IRangeFilter<T> _filter;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilterPresentationModel&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="rangeFilter">The range filter.</param>
        public RangeFilterPresentationModel(IRangeFilter<T> rangeFilter)
        {
            if (rangeFilter == null)
            {
                throw new ArgumentNullException("rangeFilter");
            }
            _filter = rangeFilter;
            _filter.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
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
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public T From
        {
            get
            {
                return _filter.From;
            }
            set
            {
                _filter.From = value;
            }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public T To
        {
            get
            {
                return _filter.To;
            }
            set
            {
                _filter.To = value;
            }
        }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public IFilter Filter
        {
            get { return _filter; }
        }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        object IRangeFilterPresentationModel.From
        {
            get
            {
                return _filter.From;
            }
            set
            {
                _fromError = false;
                if (!(value is T))
                {
                    _fromError = true;
                    IsActive = false;
                    return;
                }
                _filter.From = (T)value;
            }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        object IRangeFilterPresentationModel.To
        {
            get
            {
                return _filter.To;
            }
            set
            {
                _toError = false;
                if (!(value is T))
                {
                    _toError = true;
                    IsActive = false;
                    return;
                }

                if (((T)value).CompareTo(_filter.From) < 0)
                {
                    _toError = true;
                    IsActive = false;
                    return;
                }
                _filter.To = (T)value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this filter is active.
        /// </summary>
        /// <value><c>true</c> if this filter is active; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// Gives the UI the option to update its data.
        /// </summary>
        /// <param name="isDisplaying">if set to <c>true</c> [is displaying].</param>
        public void UpdateData(bool isDisplaying)
        { }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged(this, e);
        }
    }
}
