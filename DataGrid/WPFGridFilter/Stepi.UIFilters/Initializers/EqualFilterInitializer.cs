using System;
using System.ComponentModel;
using System.Reflection;
using Stepi.Collections;
using Stepi.Collections.Filters;

namespace Stepi.UIFilters.Initializers
{
    /// <summary>
    /// 
    /// </summary>
    public class EqualFilterInitializer : IFilterViewInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        private const string _filterName = "Equality";

        /// <summary>
        /// Determines whether the initializer can handle the specified property info.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns>
        /// 	<c>true</c> if the specified property info is applying; otherwise, <c>false</c>.
        /// </returns>
        public bool IsApplying(PropertyInfo propertyInfo)
        {
            CheckArgument(propertyInfo);
            return true;
        }

        /// <summary>
        /// Gets the UI filter view.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public IFilterView GetFilterView(PropertyInfo propertyInfo, ICollectionView collection)
        {
            CheckArgument(propertyInfo);
            IFilter filter = GetFilter(propertyInfo);

            Type multiValuePresenterType = typeof(MultiValuePresentationModel<>).MakeGenericType(propertyInfo.PropertyType);
            IMultiValuePresentationModel model = (IMultiValuePresentationModel)Activator.CreateInstance(multiValuePresenterType, filter, FilterName, collection);

            MultiValueFilterView view = new MultiValueFilterView();
            view.Model = model;
            return view;
        }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns></returns>
        protected virtual IFilter GetFilter(PropertyInfo propertyInfo)
        {
            return (IFilter)Activator.CreateInstance(typeof(EqualFilter<>).MakeGenericType(propertyInfo.PropertyType), propertyInfo);
        }

        /// <summary>
        /// Gets the name of the filter.
        /// </summary>
        /// <value>The name of the filter.</value>
        protected virtual string FilterName
        {
            get { return _filterName; }
        }

        /// <summary>
        /// Checks if the argument is not null/
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        private static void CheckArgument(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
        }
    }
}
