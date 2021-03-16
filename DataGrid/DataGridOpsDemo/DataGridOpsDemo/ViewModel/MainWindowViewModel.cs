using System.Collections.ObjectModel;
using System.Windows.Input;
using DataGridOpsDemo.ViewModel.Services;
using DataGridOpsDemo.ViewModel.Commands;

namespace DataGridOpsDemo
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        // Property variables
        private ObservableCollection<GroceryItem> p_GroceryList;
        private int p_ItemCount;

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            this.Initialize();
        }

        #endregion

        #region Command Properties

        /// <summary>
        /// Deletes the currently-selected item from the Grocery List.
        /// </summary>
        public ICommand DeleteItem { get; set; }

        #endregion

        #region Data Properties

        /// <summary>
        /// A grocery list.
        /// </summary>
        public ObservableCollection<GroceryItem> GroceryList
        {
            get { return p_GroceryList; }

            set
            {
                p_GroceryList = value;
                base.RaisePropertyChangedEvent("GroceryList");
            }
        }

        /// <summary>
        /// The currently-selected grocery item.
        /// </summary>
        public GroceryItem SelectedItem { get; set; }

        /// <summary>
        /// The number of items in the grocery list.
        /// </summary>

        public int ItemCount
        {
            get { return p_ItemCount; }

            set
            {
                p_ItemCount = value;
                base.RaisePropertyChangedEvent("ItemCount");
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Updates the ItemCount Property when the GroceryList collection changes.
        /// </summary>
        void OnGroceryListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update item count
            this.ItemCount = this.GroceryList.Count;

            // Resequence list
            SequencingService.SetCollectionSequence(this.GroceryList);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this application.
        /// </summary>
        private void Initialize()
        {
            // Initialize commands
            this.DeleteItem = new DeleteItemCommand(this);

            // Create grocery list
            p_GroceryList = new ObservableCollection<GroceryItem>();

            // Subscribe to CollectionChanged event
            p_GroceryList.CollectionChanged += OnGroceryListChanged;

            // Add items to the list
            p_GroceryList.Add(new GroceryItem("Macaroni"));
            p_GroceryList.Add(new GroceryItem("Shredded Wheat"));
            p_GroceryList.Add(new GroceryItem("Fish Filets"));
            p_GroceryList.Add(new GroceryItem("Hamburger Buns"));
            p_GroceryList.Add(new GroceryItem("Whipped Cream"));
            p_GroceryList.Add(new GroceryItem("Soft Drinks"));
            p_GroceryList.Add(new GroceryItem("Bread"));
            p_GroceryList.Add(new GroceryItem("Ice Cream"));
            p_GroceryList.Add(new GroceryItem("Chocolate Pudding"));
            p_GroceryList.Add(new GroceryItem("Sliced Turkey"));
            p_GroceryList.Add(new GroceryItem("Turkey Dressing"));
            p_GroceryList.Add(new GroceryItem("Cranberry Sauce"));
            p_GroceryList.Add(new GroceryItem("Swiss Cheese"));
            p_GroceryList.Add(new GroceryItem("Mushrooms"));
            p_GroceryList.Add(new GroceryItem("Butter"));
            p_GroceryList.Add(new GroceryItem("Eggs"));
            p_GroceryList.Add(new GroceryItem("Potatoes"));
            p_GroceryList.Add(new GroceryItem("Onion"));

            // Initialize list index
            this.GroceryList = SequencingService.SetCollectionSequence(this.GroceryList);

            // Update bindings
            base.RaisePropertyChangedEvent("GroceryList");
        }

        #endregion
    }
}