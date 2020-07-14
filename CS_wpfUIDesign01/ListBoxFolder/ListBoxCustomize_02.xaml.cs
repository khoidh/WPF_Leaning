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
    /// Interaction logic for ListBoxCustomize_02.xaml
    /// </summary>
    public partial class ListBoxCustomize_02 : Window
    {
        public ListBoxCustomize_02()
        {
            InitializeComponent();
            List<TodoItem> items = new List<TodoItem>();
            items.Add(new TodoItem() { Title = "Complete this WPF tutorial" });
            items.Add(new TodoItem() { Title = "Learn C#"});
            items.Add(new TodoItem() { Title = "Wash the car"});
            items.Add(new TodoItem() { Title = "Wash C# car"});

            lbTodoList.ItemsSource = items;
        }
    }
}
