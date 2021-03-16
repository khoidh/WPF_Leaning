using System;
using System.Reflection;

namespace Stepi.Collections.Filters
{
    /// <summary>
    /// Defines the greater or equal filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GreaterOrEqual<T>: Filter, ICompareFilter<T>
        where T:IComparable
    {
        /// <summary>
        /// instance of the value to compare to
        /// </summary>
        private T _compareTo = default(T);

        public GreaterOrEqual(PropertyInfo propertyInfo)
            : base(propertyInfo)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterOrEqual&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="compareTo">The compare to.</param>
        public GreaterOrEqual(PropertyInfo propertyInfo, T compareTo)
            : this(propertyInfo)
        {
            if(compareTo == null)
            {
                throw new ArgumentNullException("compareTo");
            }
            _compareTo = compareTo;
        }

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsMatch(object target)
        {
            if (target == null)
            {
                return false;
            }

            T value = (T)PropertyInfo.GetValue(target, null);
            return value.CompareTo(_compareTo) >= 0;
        }

        /// <summary>
        /// Gets or sets the compare to value.
        /// </summary>
        /// <value>The compare to.</value>
        public T CompareTo
        {
            get { return _compareTo; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CompareTo");
                }
                _compareTo = value;
                RaiseFilteringChanged();
            }
        }

    }
}
