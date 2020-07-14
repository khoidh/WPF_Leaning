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
    /// Interaction logic for ListBoxControl_03.xaml
    /// </summary>
    public partial class ListBoxControl_03 : Window
    {
        public ListBoxControl_03()
        {
            InitializeComponent();
            items = new List<TodoItem>();
            items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
            items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
            items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });
            //this.DataContext = items;
            lbTodoList.ItemsSource = items;
        }

        List<TodoItem> items = new List<TodoItem>();
        TodoItem todoItem = new TodoItem();
    }
}
