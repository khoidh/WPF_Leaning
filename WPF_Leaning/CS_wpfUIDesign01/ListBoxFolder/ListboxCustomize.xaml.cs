using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace CS_wpfUIDesign01
{
    /// <summary>
    /// Interaction logic for ListboxCustomize.xaml
    /// </summary>
    public partial class ListboxCustomize : Window
    {
        public ListboxCustomize()
        {
            InitializeComponent();

            List<Employee> employeeList = new List<Employee>();
            employeeList.Add(new Employee {
                //Image =  new Bitmap(global::CS_wpfUIDesign01.Properties.Resources.Employee_01),
                Image = new Uri("pack://application:,,,/Resources/Employee_01.png"),
                Firstname = "Khoi",
                Lastname = "Dam",
                Age = "12",
                Role ="Cong Nhan"
            });
            employeeList.Add(new Employee
            {
                //Image = new Uri(@"D:\03.LAP_TRINH\01.C_SHARP\WPF\1.PROJECT\WPF_Learning\CS_wpfUIDesign01\Resources\Employee_01.png"),
                Image = new Uri("pack://application:,,,/Resources/Employee_02.png"),
                Firstname = "Thanh",
                Lastname = "Nguyen",
                Age = "12",
                Role = "Cong Nhan"
            });
            //this.DataContext = employeeList;
            Listbox01.ItemsSource = employeeList;
        }

    }

    public class Employee
    {
        public Uri Image { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Age { get; set; }
        public string Role { get; set; }    //vai trò
    }
}
