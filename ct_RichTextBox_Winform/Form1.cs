using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ct_RichTextBox_Winform {
  public partial class Form1:Form {
	public Form1() {
	  InitializeComponent();
	}

	private void btViewRtf_Click(object sender,EventArgs e) {
	  viewRtf_rtbOutput.Rtf = viewRtf_rtbInput.Text;
	}

	private void t02_Convert_Click(object sender,EventArgs e) {
	  t02_rtbRtfOutput.Text = t02_rtbDoc.Rtf;
	}
  }
}
