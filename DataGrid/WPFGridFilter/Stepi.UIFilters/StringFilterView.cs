using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Stepi.Collections.Filters;

namespace Stepi.UIFilters
{
    /// <summary>
    /// Defines the string filter view
    /// </summary>
    [TemplatePart(Name = StringFilterView.PART_FilterType, Type = typeof(Selector))]
    [TemplatePart(Name = StringFilterView.PART_InputValue, Type = typeof(TextBox))]
    public class StringFilterView : FilterViewBase<StringFilterPresentationModel>, IFilterView
    {
        internal const string PART_InputValue = "PART_Input";
        internal const string PART_FilterType = "PART_FilterType";

        /// <summary>
        /// Instance of a selector allowing to choose the filtering mode
        /// </summary>
        private Selector _selectorFilterType;

        /// <summary>
        /// instance of the text box allowing you to set the value to look for
        /// </summary>
        private TextBox _inputValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilterView"/> class.
        /// </summary>
        public StringFilterView()
        {
            DefaultStyleKey = typeof(StringFilterView);
        }

        /// <summary>
        /// Called when the control template is applied to this control
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _selectorFilterType = GetTemplateChild(PART_FilterType) as Selector;
            if (_selectorFilterType != null)
            {
                _selectorFilterType.ItemsSource = GetFilterTypes();
            }

            _inputValue = GetTemplateChild(PART_InputValue) as TextBox;
            ClearBindings();
            InitialiseBindings();
        }

        /// <summary>
        /// Gets the filter types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetFilterTypes()
        {
            FieldInfo[] values = typeof(StringFilterMode).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            if (values == null)
            {
                return null;
            }
            List<string> enumValues = new List<string>();
            foreach (FieldInfo value in values)
            {
                enumValues.Add(value.Name);
            }
            return enumValues;
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public override StringFilterPresentationModel Model
        {
            get { return base.Model; }
            set
            {
                if (base.Model != value)
                {
                    ClearBindings();
                    base.Model = value;
                    InitialiseBindings();
                }
            }
        }

        /// <summary>
        /// Initialises the bindings.
        /// </summary>
        private void InitialiseBindings()
        {
            StringFilterPresentationModel model = Model;
            if (model != null)
            {
                if (_inputValue != null)
                {
                    _inputValue.SetBinding(TextBox.TextProperty, CreateBinding(model, "Value"));
                }

                if (_selectorFilterType != null)
                {
                    Binding binding = CreateBinding(model, "FilterMode");
                    binding.Converter = new EnumConverter();
                    _selectorFilterType.SetBinding(Selector.SelectedItemProperty, binding);
                }
            }
        }

        /// <summary>
        /// Creates the binding.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private static Binding CreateBinding(StringFilterPresentationModel model, string path)
        {
            Binding binding = new Binding();
            binding.Source = model;
            binding.Mode = BindingMode.TwoWay;
            binding.Path = new PropertyPath(path);
            return binding;
        }

        private void ClearBindings()
        {
            //if (_selectorFilterType != null)
            //{
            //    _selectorFilterType.SetBinding(Selector.SelectedItemProperty, new Binding() );
            //}
            //if (_inputValue != null)
            //{
            //    _inputValue.SetBinding(TextBox.TextProperty, null);
            //}
        }
    }
}
