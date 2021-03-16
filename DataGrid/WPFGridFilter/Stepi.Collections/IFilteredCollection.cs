using System;
using System.ComponentModel;

namespace Stepi.Collections
{
    /// <summary>
    /// The contract for the IFilteredCollection
    /// </summary>
    public interface IFilteredCollection : ICollectionView
    {
        /// <summary>
        /// Adds the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        void AddFilter(IFilter filter);

        /// <summary>
        /// Removes the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        void RemoveFilter(IFilter filter);

        /// <summary>
        /// Gets the type of a collection element.
        /// </summary>
        /// <value>The type.</value>
        Type Type
        {
            get;
        }
    }
}
