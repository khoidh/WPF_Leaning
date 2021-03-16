using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Stepi.UIFilters;
using Stepi.Collections;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls.Primitives;

namespace Test.GridSorting
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWnd : Window
    {
        public MainWnd()
        {
            InitializeComponent();

            DataContext = new PresentationModel();
            
        }
    }
}
