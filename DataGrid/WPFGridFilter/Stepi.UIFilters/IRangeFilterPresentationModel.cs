using System;
using System.ComponentModel;

namespace Stepi.UIFilters
{
    public interface IRangeFilterPresentationModel : IUIFilterPresentationModel
    {
        object From
        {
            get;
            set;
        }

        object To
        {
            get;
            set;
        }
    }

    public interface IRangePresentationModel<T> : IRangeFilterPresentationModel
        where T : IComparable
    {
        new T From
        {
            get;
            set;
        }

        new T To
        {
            get;
            set;
        }
    }
}
