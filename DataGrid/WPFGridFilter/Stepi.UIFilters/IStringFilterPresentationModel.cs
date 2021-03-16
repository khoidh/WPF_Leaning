using Stepi.Collections.Filters;

namespace Stepi.UIFilters
{
    public interface IStringFilterPresentationModel : IUIFilterPresentationModel
    {
        StringFilterMode FilterMode
        {
            get;
            set;
        }

        string Value
        {
            get;
            set;
        }
    }
}
