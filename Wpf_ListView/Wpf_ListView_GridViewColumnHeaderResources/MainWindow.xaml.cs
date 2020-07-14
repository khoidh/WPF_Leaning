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

namespace Wpf_ListView_GridViewColumnHeaderResources
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<User> items = new List<User>();
            items.Add(new User() { Name = "Khoi", Age = 42, Mail = "damhuykhoi@gmail.com" });
            items.Add(new User() { Name = "Nghia", Age = 12, Mail = "damhuykhoi@gmail.com" });
            items.Add(new User() { Name = "Luyen", Age = 22, Mail = "damhuykhoi@gmail.com" });

            lvDataBinding.ItemsSource = items;

        }
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Mail { get; set; }

        public override string ToString()
        {
            return this.Name + "," + this.Age + " years old";
        }
    }

}
