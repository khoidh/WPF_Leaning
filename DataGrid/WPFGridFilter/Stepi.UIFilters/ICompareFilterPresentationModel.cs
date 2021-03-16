using System;

namespace Stepi.UIFilters
{
    public interface ICompareFilterPresentationModel : IUIFilterPresentationModel
    {
        object Target
        {
            get;
            set;
        }
    }

    public interface ICompareFilterPresentationModel<T> : ICompareFilterPresentationModel
        where T:IComparable
    {
        new T Target
        {
            get;
            set;
        }
    }
}
