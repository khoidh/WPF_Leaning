using System.Reflection;

namespace Stepi.Collections.Filters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotEqualFilter<T> :EqualFilter<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotEqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public NotEqualFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
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
            return !base.IsMatch(target);
        }
    }
}
