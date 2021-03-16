using System;
using System.Reflection;
using Stepi.Collections;
using Stepi.Collections.Filters;
using System.ComponentModel;

namespace Stepi.UIFilters.Initializers
{
    public class RangeFilterInitializer : IFilterViewInitializer
    {

        public bool IsApplying(PropertyInfo propertyInfo)
        {
            CheckArgument(propertyInfo);
            return propertyInfo.PropertyType != typeof(string) && typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType);
        }

        public IFilterView GetFilterView(PropertyInfo propertyInfo, ICollectionView collection)
        {
            if (!IsApplying(propertyInfo))
            {
                throw new ArgumentException("Invalid property. Expecting the property type to be IComparable and not string");
            }

            Type filterType = typeof(RangeFilter<>).MakeGenericType(propertyInfo.PropertyType);
            IFilter filter = (IFilter)Activator.CreateInstance(filterType,propertyInfo);

            RangeFilterView view = new RangeFilterView();
            
            Type rangeModelType = typeof(RangeFilterPresentationModel<>).MakeGenericType(propertyInfo.PropertyType);
            view.Model = (IRangeFilterPresentationModel) Activator.CreateInstance(rangeModelType,filter);

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
