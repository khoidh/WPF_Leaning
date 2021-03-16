using System;
using System.Reflection;
using Stepi.Collections;
using Stepi.Collections.Filters;

namespace Stepi.UIFilters.Initializers
{
    public class NotEqualFilterInitializer : EqualFilterInitializer
    {
        private const string _filterName = "Not Equal";

        protected override sealed IFilter GetFilter(PropertyInfo propertyInfo)
        {
            return (IFilter)Activator.CreateInstance(typeof(NotEqualFilter<>).MakeGenericType(propertyInfo.PropertyType), propertyInfo);
        }

        protected override string FilterName
        {
            get
            {
                return _filterName;
            }
        }
    }
}
