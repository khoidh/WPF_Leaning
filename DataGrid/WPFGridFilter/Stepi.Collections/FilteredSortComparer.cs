using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace Stepi.Collections
{
    /// <summary>
    /// The comparer class used by FilteredCollection to keep the internal collection sorted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FilteredSortComparer<T> : IComparer<T>, IComparer
    {
        /// <summary>
        /// 
        /// </summary>
        private List<ComparerInfo> _comparers = new List<ComparerInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredSortComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="sortDescriptions">The sort descriptions.</param>
        internal FilteredSortComparer(IEnumerable<SortDescription> sortDescriptions)
        {
            if (sortDescriptions == null)
            {
                throw new ArgumentNullException("sortDescription");
            }

            Initialize(sortDescriptions);
        }

        /// <summary>
        /// Initializes the specified sort descriptions.
        /// </summary>
        /// <param name="sortDescriptions">The sort descriptions.</param>
        private void Initialize(IEnumerable<SortDescription> sortDescriptions)
        {
            Type type = typeof(T);
            foreach (SortDescription sortDescription in sortDescriptions)
            {
                //sortDescription.
                PropertyInfo pi = type.GetProperty(sortDescription.PropertyName);
                if (pi == null)
                {
                    throw new ArgumentException(string.Format("Invalid sort description. type {0} does not contain property {1}!", 
                                                               type, sortDescription.PropertyName));
                }
                else
                {
                    //get the default comparer for the type of the property info
                    IComparer comparer = (IComparer)typeof(Comparer<>).MakeGenericType(new Type[] { pi.PropertyType }).GetProperty("Default").GetValue(null, null);
                    _comparers.Add(new FilteredSortComparer<T>.ComparerInfo(pi, comparer, sortDescription.Direction == ListSortDirection.Ascending));
                }

            }
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value
        /// Condition
        /// Less than zero
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// Zero
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// Greater than zero
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        public int Compare(T x, T y)
        {
            //Debug.WriteLine(string.Format(" Compare {0} vs {1}", x, y));
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            for (int i = 0; i < _comparers.Count; ++i)
            {
                ComparerInfo ci = _comparers[i];
                int comparison = ci.Comparer.Compare(ci.GetValue(x), ci.GetValue(y));
                if (comparison != 0)
                {
                    if (!ci.IsAscending)
                    {
                        comparison = -comparison;
                    }
                    return comparison;
                }
            }
            return 0;
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value
        /// Condition
        /// Less than zero
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// Zero
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// Greater than zero
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// Neither <paramref name="x"/> nor <paramref name="y"/> implements the <see cref="T:System.IComparable"/> interface.
        /// -or-
        /// <paramref name="x"/> and <paramref name="y"/> are of different types and neither one can handle comparisons with the other.
        /// </exception>
        public int Compare(object x, object y)
        {
            return Compare((T)x, (T)y);
        }

        private class ComparerInfo
        {
            private readonly PropertyInfo _propertyInfo;
            private readonly IComparer _comparer;
            private readonly bool _isAscending;

            public ComparerInfo(PropertyInfo propertyInfo, IComparer comparer, bool isAscending)
            {
                _propertyInfo = propertyInfo;
                _comparer = comparer;
                _isAscending = isAscending;
            }

            public object GetValue(T t)
            {
                return _propertyInfo.GetValue(t, null);
            }

            public IComparer Comparer
            {
                get { return _comparer; }
            }

            public bool IsAscending
            {
                get { return _isAscending; }
            }
        }
    }
}
