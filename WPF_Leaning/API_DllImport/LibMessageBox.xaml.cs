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
using System.Runtime.InteropServices;  

namespace API_DllImport
{
    /// <summary>
    /// Interaction logic for API_MessageBox.xaml
    /// </summary>
    public partial class API_MessageBox : Window
    {
        public API_MessageBox()
        {
            InitializeComponent();
        }

        #region Import Dll
        [DllImport("User32.dll")]
        public static extern int MessageBox(int a, string b, string c, int type);

        #endregion End: ImPort Dll


        private void btShowMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox(0, "API Message Box", "API Demo", 0);
        }


        private void btGetSystemInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
