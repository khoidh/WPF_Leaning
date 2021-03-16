using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;
using Stepi.Collections;

namespace Test.GridSorting
{
    class PresentationModel:INotifyPropertyChanged
    { 
        private const uint _itemsCount = 10;
        private ObservableCollection<Data> _data = new ObservableCollection<Data>();
        private ICollectionView _collectionView;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private readonly DispatcherTimer _dataUpdateTimer;
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
        private readonly Random _random = new Random((int)DateTime.Now.Ticks);

        private readonly int  _threadId;

        public PresentationModel()
        {
            _collectionView = new FilteredCollectionView<Data>(_data);
            InitializeData();

            _dataUpdateTimer = new DispatcherTimer();
            _dataUpdateTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _dataUpdateTimer.Tick += new EventHandler(OnUpdateTimerTick);

            _threadId = Thread.CurrentThread.ManagedThreadId;
        }

        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            Debug.Assert(_threadId == Thread.CurrentThread.ManagedThreadId, "Cross thread call");
            int index = _random.Next(0, (int)_itemsCount);
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(new ThreadStart(() =>
                {
                    _data[index].Value = _random.NextDouble() * 10000 / DateTime.Now.Millisecond;
                }), null);
                return;
            }
            _data[index].Value = _random.NextDouble() * 10000 / DateTime.Now.Millisecond;
        }

        public bool IsFakeUpdateEnabled
        {
            get { return _dataUpdateTimer.IsEnabled; }
            set
            {
                if (value)
                {
                    _dataUpdateTimer.Start();
                }
                else
                {
                    _dataUpdateTimer.Stop();
                }
            }
        }

        private void InitializeData()
        {
            for (uint i = 0; i < _itemsCount; i++)
            {
                _data.Add(new Data()
                {
                    FirstName = "First" + i,
                    LastName = "Last" + i,
                    Age = i,
                    Birthday = DateTime.Today.AddYears(-((int)i)),
                    
                });
            }
        }

        public ICollectionView Data
        {
            get { return _collectionView; }
        }


    }
}
