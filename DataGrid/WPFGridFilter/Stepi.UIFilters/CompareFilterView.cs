using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;

namespace Stepi.UIFilters
{
    [TemplatePart(Name=CompareFilterView.PART_Input, Type=typeof(TextBox))]
    public class CompareFilterView : FilterViewBase<ICompareFilterPresentationModel>
    {
        internal const string PART_Input = "PART_Input";

        private TextBox _txtInput;

        public CompareFilterView()
        {
            DefaultStyleKey = typeof(CompareFilterView);
        }

        public override ICompareFilterPresentationModel Model
        {
            get { return base.Model; }
            set
            {
                if (base.Model != value)
                {
                    ClearBindings();
                    base.Model = value;
                    InitializeBindings();
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //if (_txtInput != null)
            //{
            //    _txtInput.BindingValidationError -= new System.EventHandler<ValidationErrorEventArgs>(OnBindingValidationError);
            //}
            _txtInput = GetTemplateChild(PART_Input) as TextBox;
            InitializeBindings();
        }

        private void ClearBindings()
        {
            if (_txtInput != null)
            {
                //can not have null as binding.
                _txtInput.SetBinding(TextBox.TextProperty, new Binding());
            }
        }

        private void InitializeBindings()
        {
            ICompareFilterPresentationModel model = Model;
            if (model != null && _txtInput != null)
            {
                Binding binding = CreateBinding(model);
                _txtInput.SetBinding(TextBox.TextProperty, binding);
                //_txtInput.BindingValidationError += new System.EventHandler<ValidationErrorEventArgs>(OnBindingValidationError);
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
            ICompareFilterPresentationModel model = Model;
            if (model != null)
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
        /// Creates the binding.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private static Binding CreateBinding(ICompareFilterPresentationModel model)
        {
            Binding binding = new Binding();
            binding.NotifyOnValidationError = true;
            binding.ValidatesOnExceptions = true;
            binding.Path = new PropertyPath("Target");
            binding.Source = model;
            binding.Mode = BindingMode.TwoWay;
            binding.ValidationRules.Add(new DataErrorValidationRule());
            binding.ValidationRules.Add(new ExceptionValidationRule());
            return binding;
        }
    }
}
