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

namespace Event_Command {
  /// <summary>
  /// Interaction logic for CutPageCommand.xaml
  /// </summary>
  public partial class CutPageCommand:Window {
	public CutPageCommand() {
	  InitializeComponent();
	}

	private void CutCommand_CanExecute(object sender,CanExecuteRoutedEventArgs e) {
	  e.CanExecute = (txtEditor!=null && txtEditor.SelectionLength > 0);
	}

	private void CutCommand_Execute(object sender,ExecutedRoutedEventArgs e) {
	  txtEditor.Cut();
	}

	private void PasteCommand_CanExecuted(object sender,CanExecuteRoutedEventArgs e) {
	  e.CanExecute = Clipboard.ContainsText();
	}

	private void PasteCommand_Executed(object sender,ExecutedRoutedEventArgs e) {
	  txtEditor.Paste();
	}
  }
}
