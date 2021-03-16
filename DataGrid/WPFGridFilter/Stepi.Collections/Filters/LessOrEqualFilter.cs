using System;
using System.Reflection;

namespace Stepi.Collections.Filters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LessOrEqualFilter<T> : Filter, ICompareFilter<T>
        where T:IComparable
    {
        /// <summary>
        /// instance of the value used in the comparison
        /// </summary>
        private T _compareTo =default(T);

        /// <summary>
        /// Initializes a new instance of the <see cref="LessOrEqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public LessOrEqualFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LessOrEqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="compareTo">The compare to.</param>
        public LessOrEqualFilter(PropertyInfo propertyInfo, T compareTo)
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
            return value.CompareTo(_compareTo) <= 0;
        }

        /// <summary>
        /// Gets or sets the value used in the comparison.
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
