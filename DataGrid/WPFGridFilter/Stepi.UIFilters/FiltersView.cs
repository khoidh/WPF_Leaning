using System.Windows;
using System.Windows.Controls;

namespace Stepi.UIFilters
{
    [TemplatePart(Name = FiltersView.PART_DropDown, Type = typeof(DropDownButton))]
    public class FiltersView : Control
    {
        internal const string PART_DropDown = "PART_DropDown";

        private DropDownButton _filterViewItems = null;

        static FiltersView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FiltersView), new FrameworkPropertyMetadata(typeof(FiltersView)));
        }

        public FiltersView()
        {
           DefaultStyleKey = typeof(FiltersView);
     
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _filterViewItems = GetTemplateChild(PART_DropDown) as DropDownButton;
            if (_filterViewItems != null)
            {
                //ComboBox cb = _filterViewItems as ComboBox;
                //if (cb != null)
                //{
                //    cb.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged);
                //}
                _filterViewItems.GotFocus += new RoutedEventHandler(OnGotFocus);
                _filterViewItems.LostFocus += new RoutedEventHandler(OnLostFocus);
                //_filterViewItems.DropDown = new ContextMenu();
                //check the Model
                FiltersViewPresentationModel model = Model;
                if (model != null && _filterViewItems.DropDown != null)
                {
                    _filterViewItems.DropDown.ItemsSource = model.Filters;
                }
            }
        }

        //void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox cb = sender as ComboBox;
        //    cb.SelectedIndex = -1;
        //}

        void OnLostFocus(object sender, RoutedEventArgs e)
        {
            FiltersViewPresentationModel model = Model;
            if(model != null)
            {
               model.IsDisplaying = false;
            }
        }

        void OnGotFocus(object sender, RoutedEventArgs e)
        {
            FiltersViewPresentationModel model = Model;
            if (model != null)
            {
                model.IsDisplaying = true;
            }
        }

        public FiltersViewPresentationModel Model
        {
            get { return DataContext as FiltersViewPresentationModel; }
            set
            {
                if (DataContext != value)
                {
                    DataContext = value;

                    if (_filterViewItems != null)
                    {
                        _filterViewItems.DropDown.ItemsSource = value != null ? value.Filters : null;
                    }
                }
            }
        }
    }
}
