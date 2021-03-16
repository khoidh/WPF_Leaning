using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ListBoxBindingObservableCollection.Model {
  class Countries:INotifyPropertyChanged {
	public int id { get; set; }
	public string country { get; set; }
	public DateTime ondate { get; set; }

	public string imagePath { get; set; }

	private bool m_isSelected;

	public bool IsSeleced {
	  get { return m_isSelected; }
	  set { m_isSelected = value;
		OnPropertyChanged("IsSeleced");
	  }
	}

	#region INotifyPropertyChanged Members

	public event PropertyChangedEventHandler PropertyChanged;

	private void OnPropertyChanged(string propertyName) {
	  if(PropertyChanged != null) {
		PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
	  }
	}
	#endregion
  }
}
