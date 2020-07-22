using System.Windows;
using System.Windows.Controls;
using WpfListBoxMVVC.Model;
using WpfListBoxMVVC.ViewModel;

namespace WpfListBoxMVVC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CountriesViewModel co;
        public MainWindow()
        {
            InitializeComponent();
            co = new CountriesViewModel();
            base.DataContext = co;
        }
        private void lstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            co.SelectedItem((Countries)item.SelectedItem);
        }
    }
}
