using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CatchGlobalException
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            var result = MessageBox.Show(ex.Message, "UnhandledException",
                MessageBoxButton.OK, MessageBoxImage.Error);

            ex = null;
        }

        // #2
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var result = MessageBox.Show(e.Exception.Message + "\n\n Do you want to die?".ToUpper(), "DispatcherUnhandledException", 
                MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (result == MessageBoxResult.No)
            {
                e.Handled = true;
            }
        }
    }
}
