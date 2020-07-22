using System;
using System.Collections.Generic;
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

namespace CS_wpfUIDesign01.ListBoxFolder
{
    /// <summary>
    /// Interaction logic for Lb_ListBoxItemStyle_LastItem.xaml
    /// </summary>
    public partial class Lb_ListBoxItemStyle_LastItem : Window
    {
        public Lb_ListBoxItemStyle_LastItem()
        {
            InitializeComponent();
        }
    }

    public class IsLastItemInContainerConverter_1 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)values[0];
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);

            return ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
