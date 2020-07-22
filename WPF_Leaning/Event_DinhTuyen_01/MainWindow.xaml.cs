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

//Link: https://phuongnguyenth.wordpress.com/2013/04/24/bai-7-xu-ly-su-kien-va-lenh-trong-wpf/

namespace Event_DinhTuyen_01
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

        private StringBuilder eventstr = new StringBuilder();
        private void HandleClick(object sender, RoutedEventArgs e)
        {
            //Lay thong tin ve doi tuong xu ly click
            FrameworkElement frame = (FrameworkElement)sender;
            eventstr.Append("Su kien su ly boi doi tuong co ten:");
            eventstr.Append(frame.Name);
            eventstr.Append("\n");

            //Lay thong tin ve nguon phat su kien Click
            FrameworkElement frame2 = (FrameworkElement)e.Source;
            //Loaij thanh phan UI
            eventstr.Append(frame2.GetType().ToString());
            //Dinh danh
            eventstr.Append("Voi ten goi:");
            eventstr.Append(frame2.Name);
            eventstr.Append("\n");

            //Lay thong tin ve phuon thuc dinh tuyen
            eventstr.Append("Su kien ve phuong thuc dinh tuyen:");
            eventstr.Append(e.RoutedEvent.RoutingStrategy);
            eventstr.Append("\n");

            //Dua thong tin ra man hinh
            Results.Text = eventstr.ToString();

            //Dung qua trinh lan truyen xuong hoac len
            e.Handled = true;
        }
    }
}
