using System.ComponentModel;
using System.Reflection;

namespace Stepi.UIFilters
{
    public interface IFilterViewInitializer
    {
        /// <summary>
        /// Determines whether the initializer can handle the specified property info.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns>
        /// 	<c>true</c> if the specified property info is applying; otherwise, <c>false</c>.
        /// </returns>
        bool IsApplying(PropertyInfo propertyInfo);

        /// <summary>
        /// Gets the UI filter view.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        IFilterView GetFilterView(PropertyInfo propertyInfo, ICollectionView collection);
    }
}
