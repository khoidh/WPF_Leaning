using System;
using System.ComponentModel;
using Stepi.Collections;
using Stepi.Collections.Filters;

namespace Stepi.UIFilters
{
    /// <summary>
    /// Defines the string filter view presentation model
    /// </summary>
    public class StringFilterPresentationModel : IStringFilterPresentationModel
    {
        private const string _name = "Text Searching";
        private static readonly PropertyChangedEventArgs _activeArgs = new PropertyChangedEventArgs("IsActive");

        /// <summary>
        /// flags if this filter is active or not
        /// </summary>
        private bool _isActive;

        /// <summary>
        /// the actual filter
        /// </summary>
        private readonly StringFilter _filter;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilterPresentationModel"/> class.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public StringFilterPresentationModel(StringFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            _filter = filter;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets or sets the filter mode.
        /// </summary>
        /// <value>The filter mode.</value>
        public StringFilterMode FilterMode
        {
            get
            {
                return _filter.Mode;
            }
            set
            {
                _filter.Mode = value;
            }
        }

        /// <summary>
        /// Gets or sets the value to look for.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return _filter.Value;
            }
            set
            {
                _filter.Value = value;
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
        /// Gets or sets a value indicating whether the filter is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
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
                    PropertyChanged(this, _activeArgs);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the filter can be applied
        /// </summary>
        /// <value><c>true</c> if this instance can apply; otherwise, <c>false</c>.</value>
        public bool CanApply
        {
            get
            {
                return true;
            }
            set { }
        }
        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="isDisplaying">if set to <c>true</c> [is displaying].</param>
        public void UpdateData(bool isDisplaying)
        {//nothing to update 
        }
    }
}
