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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ct_ScrollViewer_ScrollChanged
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

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scroll = (ScrollViewer)sender;
            this.Title = e.ViewportHeight.ToString() + "__" +
                scroll.ScrollableHeight.ToString();

            if (scroll.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                btBottom.Height = 0;
            else
                btBottom.Height = Double.NaN;
        }
    }
}
