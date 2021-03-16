using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;

namespace Stepi.UIFilters
{
    /// <summary>
    /// 
    /// </summary>
    [TemplatePart(Name=RangeFilterView.PART_FromInput, Type = typeof(TextBox))]
    [TemplatePart(Name = RangeFilterView.PART_ToInput, Type = typeof(TextBox))]
    public class RangeFilterView : FilterViewBase<IRangeFilterPresentationModel>
    {
        internal const string PART_FromInput = "PART_From";
        internal const string PART_ToInput = "PART_To";

        private TextBox _txtFrom;
        private TextBox _txtTo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilterView"/> class.
        /// </summary>
        public RangeFilterView()
        {
            DefaultStyleKey = typeof(RangeFilterView);
        }

        /// <summary>
        /// Called when [apply template].
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //if (_txtFrom != null)
            //{
            //    _txtFrom.BindingValidationError -= new System.EventHandler<ValidationErrorEventArgs>(OnBindingValidationError);
            //}
            _txtFrom = GetTemplateChild(PART_FromInput) as TextBox;

            //if (_txtTo != null)
            //{
            //    _txtTo.BindingValidationError += new System.EventHandler<ValidationErrorEventArgs>(OnBindingValidationError);
            //}
            _txtTo = GetTemplateChild(PART_ToInput) as TextBox;
            
            InitialiseBindings();
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public override IRangeFilterPresentationModel Model
        {
            get { return base.Model; }
            set
            {
                if (base.Model != value)
                {
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
            IRangeFilterPresentationModel model = Model;
            if (model != null)
            {
                if (_txtFrom != null)
                {
                    _txtFrom.SetBinding(TextBox.TextProperty, CreateTextBoxBinding(model, "From"));
                    //_txtFrom.BindingValidationError += new System.EventHandler<ValidationErrorEventArgs>(OnBindingValidationError);
                }
                if (_txtTo != null)
                {
                    _txtTo.SetBinding(TextBox.TextProperty, CreateTextBoxBinding(model, "To"));
                    //_txtTo.BindingValidationError += new System.EventHandler<ValidationErrorEventArgs>(OnBindingValidationError);
                }
            }
        }

        /// <summary>
        /// Called when [binding validation error].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.ValidationErrorEventArgs"/> instance containing the event data.</param>
        [Obsolete]
        private void OnBindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            IRangeFilterPresentationModel model = Model;
            if(model != null)
            {
                bool state = (e.Action == ValidationErrorEventAction.Removed);
                model.CanApply = state;
                if (!state)
                {
                    model.IsActive = state;
                }
            }
        }

        /// <summary>
        /// Creates the text box binding.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private static Binding CreateTextBoxBinding(IRangeFilterPresentationModel model, string path)
        {
            Binding binding = new Binding();
            binding.Source = model;
            binding.ValidatesOnExceptions = true;
            binding.NotifyOnValidationError = true;
            binding.Mode = BindingMode.TwoWay;
            binding.Path = new PropertyPath(path);
            return binding;
        }
    }
}
