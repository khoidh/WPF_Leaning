using System.Collections.Generic;

namespace Stepi.Collections.Filters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMultiValueFilter<T> : IFilter
    {
        /// <summary>
        /// Gets the available values used for filtering.
        /// </summary>
        /// <value>The values.</value>
        IList<T> Values { get; }
    }
}
