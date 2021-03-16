using System;
using System.ComponentModel;
using System.Reflection;
using Stepi.Collections;
using Stepi.Collections.Filters;

namespace Stepi.UIFilters.Initializers
{
    public class LessOrEqualFilterInitializer : IFilterViewInitializer
    {
        private const string _filterName = "Less or Equal";

        public bool IsApplying(PropertyInfo propertyInfo)
        {
            CheckArgument(propertyInfo);
            return propertyInfo.PropertyType != typeof(string) && typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType);
        }

        public IFilterView GetFilterView(PropertyInfo propertyInfo, ICollectionView collection)
        {
            if (!IsApplying(propertyInfo))
            {
                throw new ArgumentException("Invalid property type.Expecting a property with a return type of IComparable but not string");
            }

            IFilter filter = GetFilter(propertyInfo);

            CompareFilterView view = new CompareFilterView();
            Type modelType = typeof(CompareFilterPresentationModel<>).MakeGenericType(propertyInfo.PropertyType);
            view.Model = (ICompareFilterPresentationModel)Activator.CreateInstance(modelType, filter, FilterName);
            return view;
        }

        protected virtual string FilterName
        {
            get { return _filterName; }
        }

        protected virtual IFilter GetFilter(PropertyInfo propertyInfo)
        {
            Type filterType = typeof(LessOrEqualFilter<>).MakeGenericType(propertyInfo.PropertyType);
            IFilter filter = (IFilter)Activator.CreateInstance(filterType, propertyInfo);
            return filter;
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
