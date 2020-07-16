using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ValueConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class AgeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? selectedDate = value as DateTime?;
            if (selectedDate == null)
                return null;

            return selectedDate.Value.Year;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int year = int.Parse(value.ToString());
            return new DateTime(year, 1, 1);
        }
    }
}
