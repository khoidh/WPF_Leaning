using System;
using System.Windows;

namespace CatchGlobalException
{
    /// <summary>
    /// Interaction logic for ContactForm.xaml
    /// </summary>
    public partial class ContactForm : Window
    {
        public ContactForm()
        {
            InitializeComponent();
            this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var result = MessageBox.Show(e.Exception.Message + string.Format("Handled = {0}", e.Handled), "Single specific UI dispatcher thread",
                 MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newSubmit_Click(object sender, RoutedEventArgs e)
        {
          this.Dispatcher.Invoke(new Action(() =>
          {
              throw new ArgumentNullException();
          }));
        }
    }
}
