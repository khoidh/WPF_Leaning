using System;
using System.Collections;
using System.Collections.Generic;

namespace Stepi.UIFilters
{
    public interface IMultiValuePresentationModel : IUIFilterPresentationModel
    {
        IEnumerable AvailableValues { get; }
    }
}
