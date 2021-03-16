using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ListBoxBindingObservableCollection.Model;
using ListBoxBindingObservableCollection.Helpers;

namespace ListBoxBindingObservableCollection.ViewModel {
  class CountriesViewModel:INotifyPropertyChanged {
	public ObservableCollection<Countries> countriesList { get; private set; }

	private Countries m_currentCountries;

	public Countries CurrentCountries {
	  get { return m_currentCountries; }
	  set {
		if(value != m_currentCountries) {
		  m_currentCountries = value;
		  OnPropertyChanged("CurrentCountries");
		}
	  }
	}
	
	public String txtSelectedItem { get; set; }
	public String NullImage = "NullImage.png";



	private string _imageView = @"Images/57 KHUBND .signed.signed_1.jpg";

	public string ImageView {
	  get { return _imageView; }
	  set {
		_imageView = value;
		OnPropertyChanged("ImageView");
	  }
	}

	public CountriesViewModel() {
	  txtSelectedItem = "Please select a country";
	  FillList();
	  this.SelectItemCommand=new RelayCommand(this.SelectItem);

	  //ImageView = @"Images/57 KHUBND .signed.signed_1.jpg";
	}

	public void FillList() {
	  if(countriesList == null)
		countriesList = new ObservableCollection<Countries>();

	  for(int i = 0;i < 4;i++) {
		countriesList.Add(new Countries {
		  id = i,
		  country = "country" + i.ToString(),
		  ondate = DateTime.Now.AddHours(i),
		  imagePath = String.Format("Images/57 KHUBND .signed.signed_{0}.jpg",i+1)
		});

	  }
	}

	public void SelectedItem(Countries country) {
	  txtSelectedItem = "You Selected " + country.country + "(ID: " + country.id + ", added on Date : " + country.ondate + ")";
	  OnPropertyChanged("txtSelectedItem");
	}

	#region INotifyPropertyChanged Members

	public event PropertyChangedEventHandler PropertyChanged;

	private void OnPropertyChanged(string propertyName) {
	  if(PropertyChanged != null) {
		PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
	  }
	}
	#endregion

	#region Command
  
	public ICommand SelectItemCommand { get; set; }

	private void SelectItem() {
	  CurrentCountries.IsSeleced = !CurrentCountries.IsSeleced;
	}
	#endregion

  }
}
