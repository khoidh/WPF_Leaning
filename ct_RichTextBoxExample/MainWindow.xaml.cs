using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ct_RichTextBoxExample {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow:Window {
	public MainWindow() {
	  InitializeComponent();
	  txtInput.Text=@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Times New Roman;}}
\viewkind4\uc1\pard\f0\fs24 N\u7897?i dung c\'e2u con 01\par
}
";
	}

	private void btShow_Click(object sender,RoutedEventArgs e) {

	}
  }
}
