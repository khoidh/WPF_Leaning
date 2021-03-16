using System;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;

namespace Test.GridSorting
{
    public class Data : INotifyPropertyChanged
    {
        private static PropertyChangedEventArgs args = new PropertyChangedEventArgs("Value");
        private string _firstName;
        private string _lastName;
        private uint _age;
        private DateTime _birthDay;
        private double _value;

        public double Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(args);
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FirstName"));
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LastName"));
                }
            }
        }

        public DateTime Birthday
        {
            get { return _birthDay; }
            set
            {
                if (_birthDay != value)
                {
                    _birthDay = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("BirthDay"));
                }
            }
        }

        public uint Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Age"));
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", _firstName, _value);
        }


        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged(this, args);
            //Delegate[] delegates = _propertyChanged.GetInvocationList();
            //foreach (Delegate del in delegates)
            //{
            //    if (_propertyChanged.GetInvocationList().Contains(del))
            //    {
            //        del.DynamicInvoke(this, args);
            //    }
            //}
        }

        
        public event PropertyChangedEventHandler PropertyChanged = delegate{};
        //{
        //    add
        //    {
        //        //int count = _propertyChanged.GetInvocationList().Count<Delegate>(d => d.Method == value.Method);
        //        //if(count < 5)
        //        {
        //            _propertyChanged += value;
        //            Debug.WriteLine(string.Format("{1} Add Handlers : {0}", _propertyChanged.GetInvocationList().Length, _firstName));
        //        }
        //    }
        //    remove
        //    {
        //        _propertyChanged -= value;
        //        Debug.WriteLine(string.Format("{1} Remove Handlers : {0}", _propertyChanged.GetInvocationList().Length, _firstName));
        //    }
        //}

    }

}
