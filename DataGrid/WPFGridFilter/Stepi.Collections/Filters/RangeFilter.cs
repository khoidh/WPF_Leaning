using System;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;

namespace Stepi.Collections.Filters
{
    /// <summary>
    /// Defines the range filter 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RangeFilter<T> : Filter, IRangeFilter<T>
        where T : IComparable
    {
        private static readonly PropertyChangedEventArgs _argsTo = new PropertyChangedEventArgs("To");
        private static readonly PropertyChangedEventArgs _argsFrom = new PropertyChangedEventArgs("From");

        /// <summary>
        /// Minimum value
        /// </summary>
        private T _from = default(T);
        /// <summary>
        /// Maximum value
        /// </summary>
        private T _to = default(T);

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public RangeFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public RangeFilter(PropertyInfo propertyInfo, T from, T to)
            : base(propertyInfo)
        {
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }

            if (to.CompareTo(from) == -1)
            {
                throw new ArgumentException("Invalid input. <from> should be less or equal than <to>");
            }
            _to = to;
            _from = from;
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>From.</value>
        public T From
        {
            get
            {
                return _from;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("From");
                }
                if (_from.CompareTo(value) != 0)
                {
                    _from = value;
                    if (value.CompareTo(_to) > 0)
                    {
                        To = _from;
                    }
                    PropertyChanged(this, _argsFrom);
                    RaiseFilteringChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets he maximum value.
        /// </summary>
        /// <value>To.</value>
        public T To
        {
            get
            {
                return _to;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("From");
                }
                if (_to.CompareTo(value) != 0)
                {
                    _to = value;
                    if (_to.CompareTo(_from) < 0)
                    {
                        From = _to;
                    }
                    PropertyChanged(this, _argsTo );
                    RaiseFilteringChanged();
                }
            }
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

            bool result = (value.CompareTo(_from) >= 0 && value.CompareTo(_to) <= 0);
            Debug.WriteLine(string.Format("Element:{0}", target));
            Debug.WriteLine(string.Format(" {0} ?<= {1} ?<= {2} : {3}", _from, value, _to, result));
            return result;
        }
    }
}
