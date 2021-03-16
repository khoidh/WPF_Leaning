using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WpfListBoxMVVC.Model;

namespace WpfListBoxMVVC.ViewModel
{
    class CountriesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Countries> countries { get; set; }
        public String txtSelectedItem { get; set; }

        public CountriesViewModel()
        {
            txtSelectedItem = "Please select a country";
            FillList();
        }

        public void FillList()
        {
            if (countries == null)
                countries = new ObservableCollection<Countries>();

            for (int i = 0; i < 10; i++)
            {
                countries.Add(new Countries
                {
                    id = i,
                    country = "country" + i.ToString(),
                    ondate = DateTime.Now.AddHours(i)
                });
            }
        }

        public void SelectedItem(Countries country)
        {
            txtSelectedItem = "You Selected " + country.country + "(ID: " + country.id + ", added on Date : " + country.ondate + ")";
            OnPropertyChanged("txtSelectedItem");
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
