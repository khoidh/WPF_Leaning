using System;
using System.ComponentModel;
using System.Windows;

namespace Stepi.UIFilters
{
    /// <summary>
    /// Defines the class used by the multi filter view.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UIFilterValueItem<T> :   IUIFilterValueItem<T>
    {
        private readonly static PropertyChangedEventArgs _selectedChangedArgs = new PropertyChangedEventArgs("IsSelected");

        //private static readonly DependencyProperty IsSelectedProperty;

        //static UIFilterValueItem()
        //{
        //    IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(UIFilterValueItem<T>), new PropertyMetadata(false));
        //}

        /// <summary>
        /// the value is selected
        /// </summary>
        private bool _isSelected ;

        /// <summary>
        /// instance of the value
        /// </summary>
        private readonly T _value;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="UIFilterValueItem&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public UIFilterValueItem(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
                //return (bool) GetValue(IsSelectedProperty);
            }
            set
            {
                //SetValue(IsSelectedProperty, value);
                if (_isSelected != value)
                {
                    _isSelected = value;
                    PropertyChanged(this, _selectedChangedArgs);
                }
            }
        }

        /// <summary>
        /// Gets the value held by this class.
        /// </summary>
        /// <value>The value.</value>
        public T Value
        {
            get
            {
                return _value;
            }
        }


        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        object IUIFilterValueItem.Value
        {
            get { return Value; }
        }

    }
}
