using System;
using System.Threading;
using System.Windows;

namespace CatchGlobalException
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                try
                {
                    throw new NotImplementedException();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newContact_Click(object sender, RoutedEventArgs e)
        {
            new ContactForm().ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void throwException_Click(object sender, RoutedEventArgs e)
        {
            var work = new Thread(() =>
            {
                Thread.Sleep(1000);
                throw new ThreadInterruptedException();
            });

            work.Start();
        }
    }

}
