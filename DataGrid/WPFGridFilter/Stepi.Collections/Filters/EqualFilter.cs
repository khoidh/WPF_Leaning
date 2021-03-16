using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Stepi.Collections.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EqualFilter<T> : Filter, IMultiValueFilter<T>
    {
        /// <summary>
        /// Available values to check for equality
        /// </summary>
        private ObservableCollection<T> _values = new ObservableCollection<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public EqualFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            if (propertyInfo.PropertyType != typeof(T))
            {
                throw new ArgumentException("Invalid property type, the return type is not matching the class generic type");
            }
            _values.CollectionChanged += delegate { RaiseFilteringChanged(); };
        }

        /// <summary>
        /// Gets the values to check for equality
        /// </summary>
        /// <value>The values.</value>
        public IList<T> Values
        {
            get { return _values; }
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

            return _values.Contains(value);
        }
    }
}