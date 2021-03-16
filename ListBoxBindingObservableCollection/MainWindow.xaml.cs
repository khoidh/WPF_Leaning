using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ListBoxBindingObservableCollection.Model;
using ListBoxBindingObservableCollection.ViewModel;

namespace ListBoxBindingObservableCollection {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow:Window {

	CountriesViewModel co;

	public MainWindow() {
	  InitializeComponent();
	  co = new CountriesViewModel();
	  base.DataContext = co;
	}

	private void lstBox_SelectionChanged(object sender,SelectionChangedEventArgs e) {
	  var item = (ListBox)sender;
	  co.SelectedItem((Countries)item.SelectedItem);
	}
  }
}
