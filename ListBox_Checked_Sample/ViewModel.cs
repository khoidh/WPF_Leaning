using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedListBoxSample
{
  public  class ViewModel
    {
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>
        {
            "AAAAA", "BBBBB", "CCCCC", "DDDDD"
        };
    }
}
