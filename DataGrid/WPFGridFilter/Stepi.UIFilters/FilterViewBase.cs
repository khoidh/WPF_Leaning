using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Stepi.UIFilters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [TemplatePart(Name = FilterViewBase<T>.PART_ToggleActivate, Type = typeof(ToggleButton))]
    [TemplatePart(Name = FilterViewBase<T>.PART_Name, Type = typeof(TextBlock))]
    public abstract class FilterViewBase<T> : Control, IFilterView
        where T : class, IUIFilterPresentationModel
    {
        internal const string PART_ToggleActivate = "PART_ToggleActivate";
        internal const string PART_Name = "PART_Name";

        private ToggleButton _toggleButton;
        private TextBlock _txtName;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterViewBase&lt;T&gt;"/> class.
        /// </summary>
        public FilterViewBase()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(GetType(), new FrameworkPropertyMetadata(GetType()));
            DefaultStyleKey = GetType();
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public virtual T Model
        {
            get { return DataContext as T; }
            set
            {
                DataContext = value;
                InitializeBindings();
            }
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>The model.</value>
        IUIFilterPresentationModel IFilterView.Model
        {
            get { return Model as IUIFilterPresentationModel; }
        }

        /// <summary>
        /// Initializes the bindings.
        /// </summary>
        private void InitializeBindings()
        {
            //if (_toggleButton != null)
            //{
            //    _toggleButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            //}

            IUIFilterPresentationModel model = Model;
            if (model != null)
            {
                if (_toggleButton != null)
                {
                    Binding binding = CreateBinding(model, BindingMode.TwoWay, "IsActive");
                    _toggleButton.SetBinding(ToggleButton.IsCheckedProperty, binding);

                    binding = CreateBinding(model, BindingMode.TwoWay, "CanApply");
                    _toggleButton.SetBinding(ToggleButton.IsEnabledProperty, binding);

                }
                if (_txtName != null)
                {
                    Binding binding = CreateBinding(model, BindingMode.OneWay, "Name");
                    _txtName.SetBinding(TextBlock.TextProperty, binding);
                }
            }
        }

        /// <summary>
        /// Creates the binding.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private static Binding CreateBinding(IUIFilterPresentationModel model, BindingMode mode, string property)
        {
            Binding binding = new Binding();
            binding.Source = model;
            binding.Mode = mode;
            binding.Path = new PropertyPath(property);
            return binding;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _toggleButton = GetTemplateChild(PART_ToggleActivate) as ToggleButton;
            _txtName = GetTemplateChild(PART_Name) as TextBlock;
            InitializeBindings();
        }
    }
}
