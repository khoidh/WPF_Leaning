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

namespace CS_wpfUIDesign01.ListBoxRes
{
    /// <summary>
    /// Interaction logic for ListBoxControl_04.xaml
    /// </summary>
    public partial class ListBoxControl_04 : Window
    {
        public ListBoxControl_04()
        {
            InitializeComponent();

            List<Detail> det = new List<Detail>();

            Detail x = new Detail();
            Detail x1 = new Detail();
            Detail x2 = new Detail();

            det.Add(x);
            det.Add(x1);
            det.Add(x2);

            lbox.ItemsSource = det;
        }
    }

    public class Detail

    {
        public string Name { get; set; }
        public Detail()
        {
            Name = "Angular";
        }
    }
}
