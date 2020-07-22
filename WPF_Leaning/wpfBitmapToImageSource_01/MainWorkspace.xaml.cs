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

namespace wpfBitmapToImageSource_01
{
    /// <summary>
    /// Interaction logic for MainWorkspace.xaml
    /// </summary>
    public partial class MainWorkspace : Window
    {
        public MainWorkspace()
        {
            InitializeComponent();
        }

        private MainWindow mainwd;

        private void btnLoadChild_Click(object sender, RoutedEventArgs e)
        {
            mainwd = new MainWindow();
            mainwd.ShowDialog();
        }

        private void btnCloseChild_Click(object sender, RoutedEventArgs e)
        {
            mainwd.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
