using System;
using System.Reflection;
using Stepi.Collections;
using Stepi.Collections.Filters;

namespace Stepi.UIFilters.Initializers
{
    /// <summary>
    /// 
    /// </summary>
    public class GreterOrEqualFilterInitializer : LessOrEqualFilterInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        private const string _filterName = "Greater or Equal";

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns></returns>
        protected override IFilter GetFilter(PropertyInfo propertyInfo)
        {
            Type filterType = typeof(GreaterOrEqual<>).MakeGenericType(propertyInfo.PropertyType);
            IFilter filter = (IFilter)Activator.CreateInstance(filterType, propertyInfo);
            return filter;
        }

        /// <summary>
        /// Gets the name of the filter.
        /// </summary>
        /// <value>The name of the filter.</value>
        protected override string FilterName
        {
            get
            {
                return _filterName;
            }
        }
    }
}
