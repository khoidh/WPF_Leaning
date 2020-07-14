using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CS_wpfUIDesign01.ListBox
{
    /// <summary>
    /// Interaction logic for Lb_ListBoxItemStyle.xaml
    /// </summary>
    public partial class Lb_ListBoxItemStyle : Window
    {
        public Lb_ListBoxItemStyle()
        {
            InitializeComponent();
        }
    }


    ///Converter
    public class IsLastItemInContainerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int count = (int)values[1];

            if (values != null && values.Length == 2 && count > 0)
            {
                System.Windows.Controls.ItemsControl itemsControl = values[0] as System.Windows.Controls.ItemsControl;
                var itemContext = (values[1] as System.Windows.Controls.ContentPresenter).DataContext;

                var lastItem = itemsControl.Items[count - 1];

                return Equals(lastItem, itemContext);
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
