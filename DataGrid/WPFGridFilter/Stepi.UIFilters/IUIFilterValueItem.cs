using System;
using System.ComponentModel;

namespace Stepi.UIFilters
{
    public interface IUIFilterValueItem : INotifyPropertyChanged
    {
        //event EventHandler SelectedChanged;

        bool IsSelected { get; set; }

        object Value { get; }

    }

    public interface IUIFilterValueItem<T> : IUIFilterValueItem
    {
        //event EventHandler SelectedChanged;

        //bool IsSelected { get; set; }

       new T Value { get;  }
    }
}
