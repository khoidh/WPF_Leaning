using System;
using System.Reflection;
using Stepi.Collections.Filters;
using System.ComponentModel;

namespace Stepi.UIFilters.Initializers
{
    public class StringFilterInitializer : IFilterViewInitializer
    {
        public bool IsApplying(PropertyInfo propertyInfo)
        {
            CheckArgument(propertyInfo);
            return propertyInfo.PropertyType == typeof(string);
        }

        public IFilterView GetFilterView(PropertyInfo propertyInfo, ICollectionView collection)
        {
            if (!IsApplying(propertyInfo))
            {
                throw new ArgumentException("Invalid property specified. Property type is not string");
            }
            StringFilterView view = new StringFilterView();
            view.Model = new StringFilterPresentationModel(new StringFilter(propertyInfo, StringFilterMode.Equals));
            return view;
        }

        private static void CheckArgument(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
        }
    }
}
