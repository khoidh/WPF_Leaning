using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using Stepi.Collections;

namespace Stepi.UIFilters
{
    [TemplatePart(Name = DataGridColumnFilter.PART_FiltersView, Type = typeof(FiltersView))]
    public class DataGridColumnFilter : Control
    {
        internal const string PART_FiltersView = "PART_FiltersView";

        private FiltersView _filtersView;
        private IFilterViewInitializersManager _initializersManager = new FilterViewInitializersManager();

        public DataGridColumnFilter()
        {
            DefaultStyleKey = GetType();
            //Loaded += new RoutedEventHandler(OnLoaded);
        }

        /// <summary>
        /// Gets or sets the initalizers manager.
        /// </summary>
        /// <value>The initalizers manager.</value>
        public IFilterViewInitializersManager InitalizersManager
        {
            get { return _initializersManager; }
            set { _initializersManager = value; }
        }

        ///// <summary>
        ///// Called when the control has been rendered and is ready for interaction
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        //private void OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    DataGrid dataGrid = VisualHelper.GetParent<DataGrid>(this);
        //    if (dataGrid != null)
        //    {
        //        int count = dataGrid.Columns.Count;
        //    }
        //    InitializeFilterViewModel();
        //}

        /// <summary>
        /// Initializes the filter view model.
        /// </summary>
        private void InitializeFilterViewModel()
        {
            if (_filtersView != null)
            {
                FiltersViewPresentationModel model = GetPresentationModel();
                if (model != null)
                {
                    _filtersView.Model = model;
                }
                else
                {
                    _filtersView.Visibility = Visibility.Collapsed;
                    _filtersView.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Gets the presentation model.
        /// </summary>
        /// <returns></returns>
        private FiltersViewPresentationModel GetPresentationModel()
        {
            DataGridColumnHeader columnHeader = VisualHelper.GetParent<DataGridColumnHeader>(this);
            DataGridColumn column = columnHeader != null ? columnHeader.Column : null;
            if (columnHeader == null || column == null)
            {
                return null;
            }
            
            DataGrid dataGrid = VisualHelper.GetParent<DataGrid>(columnHeader);
            if (dataGrid == null)
            {
                return null;
            }
            if (column.DisplayIndex >= dataGrid.Columns.Count)
            {
                return null;
            }
            IFilteredCollection filteredCollection = dataGrid.ItemsSource as IFilteredCollection;
            if (!(filteredCollection != null && typeof(INotifyPropertyChanged).IsAssignableFrom(filteredCollection.Type)))
            {
                return null;
            }
            DataGridBoundColumn columnBound = column as DataGridBoundColumn;
            string bindingPath = null;
            if (columnBound == null || columnBound.Binding == null)
            {

                DataGridTemplateColumn templateColumn = column as DataGridTemplateColumn;
                if (templateColumn != null)
                {
                    //this might not always be the case
                    string header = templateColumn.Header as string;
                    if (header == null)
                    {
                        return null;
                    }
                    bindingPath = header;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Binding binding = columnBound.Binding as Binding;
                if (binding != null)
                {
                    bindingPath = binding.Path.Path;
                }
            }

            if (bindingPath.Contains(".") || string.IsNullOrEmpty(bindingPath))
            {
                return null;
            }

            PropertyInfo propertyInfo = filteredCollection.Type.GetProperty(bindingPath);
            if (propertyInfo != null)
            {
                return new FiltersViewPresentationModel(propertyInfo, filteredCollection, InitalizersManager);
            }
            return null;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _filtersView = GetTemplateChild(PART_FiltersView) as FiltersView;
            InitializeFilterViewModel();
        }
    }

}
