
using System;
using System.Reflection;

namespace Stepi.Collections
{
    /// <summary>
    /// Defines the contract for a filter used by the FilteredCollection
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Occurs when the filter has changed and the IsMatch logic has been affected.
        /// </summary>
        event EventHandler FilteringChanged;


        /// <summary>
        /// Gets the property info.
        /// </summary>
        /// <value>The property info.</value>
        PropertyInfo PropertyInfo
        {
            get;
        }

        /// <summary>
        /// Determines whether the specified target is matching the criteria
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is matching the criteria; otherwise, <c>false</c>.
        /// </returns>
        bool IsMatch(object target);
    }


    /// <summary>
    /// Generics
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilter<T> : IFilter
        where T : IComparable
    { }
}
