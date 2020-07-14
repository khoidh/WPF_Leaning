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
    /// Interaction logic for ListBox_Selection.xaml
    /// </summary>
    public partial class ListBox_Selection : Window
    {
        public ListBox_Selection()
        {
            InitializeComponent();
            List<TodoItem> items = new List<TodoItem>();
            items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
            items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
            items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });
            items.Add(new TodoItem() { Title = "Wash C# car", Completion =5 });

            lbTodoList.ItemsSource = items;
        }

        private void BtnShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (object o in lbTodoList.SelectedItems)
                MessageBox.Show((o as TodoItem).Title);
        }

        private void BtnSelectLast_Click(object sender, RoutedEventArgs e)
        {
            lbTodoList.SelectedIndex = lbTodoList.Items.Count - 1;
        }

        private void BtnSelectNext_Click(object sender, RoutedEventArgs e)
        {
            //Case multiple select : index = item fist selected
            int nextIndex = 0;
            if ((lbTodoList.SelectedIndex >= 0) && (lbTodoList.SelectedIndex < (lbTodoList.Items.Count - 1)))
                nextIndex = lbTodoList.SelectedIndex + 1;
            lbTodoList.SelectedIndex = nextIndex;
        }

        private void BtnSelectCSharp_Click(object sender, RoutedEventArgs e)
        {
            lbTodoList.SelectedItems.Clear();
            foreach (var item in lbTodoList.Items)
            {
                if (item is TodoItem && (item as TodoItem).Title.Contains("C#"))
                {
                    //lbTodoList.SelectedItem = item;
                    //break;
                    lbTodoList.SelectedItems.Add(item);
                }
            }
        }

        private void BtnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in lbTodoList.Items)
                    lbTodoList.SelectedItems.Add(item);
        }

        private void LbTodoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
