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

namespace CS_wpfUIDesign01.ListBox
{
    /// <summary>
    /// Interaction logic for ListBox_CreateCodeBehind.xaml
    /// </summary>
    public partial class ListBox_CreateCodeBehind : Window
    {

        public ListBox_CreateCodeBehind()
        {
            InitializeComponent();
            CreateListBox();
            LoadListBox1();
        }

        private static TextBlock CreateTextBlock()
        {
            return new TextBlock()
            {
                Name = "textBlock01",
                Text = "Hello world!",
                Foreground = Brushes.Red
            };
        }

        System.Windows.Controls.ListBox listBox1;
        private SolidColorBrush _BorderBrush = Brushes.Blue;
        private Thickness _BorderThickness = new Thickness(1);

        //BorderThickness = new Thickness(1),
        //BorderBrush = Brushes.Brown,
        void CreateListBox()
        {
            listBox1 = new System.Windows.Controls.ListBox()
            {
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(50),
                //BorderBrush = _BorderBrush;
                BorderThickness = new Thickness(0)
            };
            listBox1.SelectionChanged += new SelectionChangedEventHandler(listBox1_SelectionChanged);
            ContentPanel.Children.Add(listBox1);
        }

        void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = ((sender as System.Windows.Controls.ListBox).SelectedItem as ListBoxItem);
            MessageBox.Show("select Item" + lbi.Tag.ToString());
            //if (listBox1.SelectedIndex == -1) return;
            //if (sender is null) return;
            //if (sender is ListBoxItem)
            //{
            //    ListBoxItem lbItem = (sender as ListBoxItem);
            //    MessageBox.Show(lbItem.Tag.ToString());
            //}
            //StackPanel stackP = (StackPanel)listBox1.SelectedItem;

            //MessageBox.Show(listBox1.SelectedIndex.ToString());
            //Image im = (Image)stackP.Children.ElementAt(0);
            //TextBlock textB = (TextBlock)stackP.Children.ElementAt(1);
            //CheckBox checkB = (CheckBox)stackP.Children.ElementAt(2);
            //if ((bool)checkB.IsChecked)
            //{
            //    im.Source = new BitmapImage(new Uri("Background.png", UriKind.Relative));   // test image from project    
            //    textB.Text = "selected";
            //}
            //else
            //{
            //    im.Source = new BitmapImage(new Uri("ApplicationIcon.png", UriKind.Relative));   // test image from project    
            //    textB.Text = "unselected";
            //}
            //listBox1.SelectedIndex = -1;
        }

        void LoadListBox1()
        {
            for (int ctr = 0; ctr < 5; ctr++)
            {
                // text blocks   
                ListBoxItem ltItem = NewListBoxItem(ctr);
                listBox1.Items.Insert(0, ltItem);
            }
        }

        private ListBoxItem NewListBoxItem(int ctr)
        {
            TextBlock txtBlk = new TextBlock()
            {
                Name = "txtBlk" + ctr.ToString(),
                Text = "txtBlk " + ctr.ToString(),
                FontSize = 20
            };
            Border bdr = new Border()
            {
                //Margin = new Thickness(1),
                //Background = Brushes.Wheat,
                //BorderThickness = new Thickness(1),
                //BorderBrush = Brushes.Brown,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Child = txtBlk
            };

            //Add to the listbox 
            ListBoxItem ltItem = new ListBoxItem()
            {
                Content = bdr,
                Margin = new Thickness(0, 0, 0, 1),
                BorderBrush = _BorderBrush,
                BorderThickness = _BorderThickness,
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#CCFFFF")),
                Tag = ctr.ToString()
            };
            return ltItem;
        }

        private void BtnDeleteItem0_Click(object sender, RoutedEventArgs e)
        {
            var count = listBox1.Items.Count;
            if (count <= 0) return;

            listBox1.Items.RemoveAt(count - 1);
        }

        private void BtnInsertItem_Click(object sender, RoutedEventArgs e)
        {
            var count = listBox1.Items.Count;
            if (count >= 5)
            {
                listBox1.Items.RemoveAt(count-1);
            }

            var stt = 0;
            if (! int.TryParse((listBox1.Items[0] as ListBoxItem).Tag.ToString(), out stt)) return;

            ListBoxItem ltItem = NewListBoxItem(stt + 1);
            listBox1.Items.Insert(0,ltItem);
        }
    }
}
