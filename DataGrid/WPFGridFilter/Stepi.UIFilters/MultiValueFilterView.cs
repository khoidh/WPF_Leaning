using System.Windows;
using System.Windows.Controls;

namespace Stepi.UIFilters
{
    [TemplatePart(Name = MultiValueFilterView.PART_ItemsTemplateName, Type = typeof(ItemsControl))]
    public partial class MultiValueFilterView : FilterViewBase<IMultiValuePresentationModel>
    {
        internal const string PART_ItemsTemplateName = "PART_Items";

        private ItemsControl _itemsCtrl;

        public override IMultiValuePresentationModel Model
        {
            get
            {
                return base.Model;
            }
            set
            {
                base.Model = value;
                if (_itemsCtrl != null)
                {
                    _itemsCtrl.ItemsSource = value != null ? value.AvailableValues : null;
                }
            }
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _itemsCtrl = GetTemplateChild(MultiValueFilterView.PART_ItemsTemplateName) as ItemsControl;
            if (_itemsCtrl != null)
            {
                IMultiValuePresentationModel model = Model;
                if (model != null)
                {
                    _itemsCtrl.ItemsSource = model.AvailableValues;
                }
            }
        }

    }
}
