using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Stepi.UIFilters
{
    /// <summary>
    /// 
    /// </summary>
    public class DropDownButton : ToggleButton
    {
        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }
        
        public static readonly DependencyProperty DropDownProperty = DependencyProperty.Register("DropDown", typeof(ContextMenu), typeof(DropDownButton), 
                                                                                                  new UIPropertyMetadata(null));

        public static readonly DependencyProperty DropDownStyleProperty = DependencyProperty.Register("DropDownStyle", typeof(Style), typeof(DropDownButton), 
                                                                                                       new PropertyMetadata(null, OnDropDownStyleChanged));

        public DropDownButton()
        {
            //create the contextmenu; it can not be created on the static method
            DropDown = new ContextMenu();
            //create the binding
            Binding binding = new Binding("DropDown.IsOpen");
            binding.Source = this;
            SetBinding(IsCheckedProperty, binding);

            DefaultStyleKey = typeof(DropDownButton);
            
        }

        public ContextMenu DropDown
        {
            get { return (ContextMenu)GetValue(DropDownProperty); }
            set { SetValue(DropDownProperty, value); }
        }

        public Style DropDownStyle
        {
            get { return (Style)GetValue(DropDownStyleProperty); }
            set { SetValue(DropDownStyleProperty, value); }
        }

        private static void OnDropDownStyleChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            DropDownButton button = depObj as DropDownButton;
            if (button.DropDown != null)
            {
                button.DropDown.Style = e.NewValue as Style;
            }
        }
    }
}
