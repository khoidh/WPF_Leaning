using System;
using System.ComponentModel;

namespace Stepi.Collections.Filters
{
    /// <summary>
    /// Defines the contract for a range filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRangeFilter<T> : IFilter<T>, INotifyPropertyChanged
        where T :IComparable
    {
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>From.</value>
        T From
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets he maximum value.
        /// </summary>
        /// <value>To.</value>
        T To
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines the contract for a compare filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICompareFilter<T> : IFilter<T>
        where T : IComparable
    {
        /// <summary>
        /// Gets or sets the value used in the comparison.
        /// </summary>
        /// <value>The compare to.</value>
        T CompareTo
        {
            get;
            set;
        }
    }
}
