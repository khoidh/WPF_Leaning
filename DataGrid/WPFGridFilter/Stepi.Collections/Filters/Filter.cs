using System;
using System.Reflection;

namespace Stepi.Collections.Filters
{
    /// <summary>
    /// Base class for a filter
    /// </summary>
    public abstract class Filter : IFilter
    {
        private PropertyInfo _propertyInfo;

        /// <summary>
        /// Occurs when the filter has changed and the IsMatch logic has been affected.
        /// </summary>
        public event EventHandler FilteringChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        protected Filter(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            _propertyInfo = propertyInfo;
        }

        /// <summary>
        /// Raises the filtering changed event.
        /// </summary>
        protected void RaiseFilteringChanged()
        {
            FilteringChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets the property info whose property name is filtered
        /// </summary>
        /// <value>The property info.</value>
        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
        }

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsMatch(object target);
    }
}