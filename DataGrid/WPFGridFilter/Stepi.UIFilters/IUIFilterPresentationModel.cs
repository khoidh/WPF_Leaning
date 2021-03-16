using Stepi.Collections;
using System.ComponentModel;

namespace Stepi.UIFilters
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUIFilterPresentationModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        IFilter Filter { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this filter is active.
        /// </summary>
        /// <value><c>true</c> if this filter is active; otherwise, <c>false</c>.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the filter can be applied
        /// </summary>
        /// <value><c>true</c> if this instance can apply; otherwise, <c>false</c>.</value>
        bool CanApply { get; set; }

        /// <summary>
        /// Gets the name(description) of the filter
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gives the UI the option to update its data.
        /// </summary>
        /// <param name="isDisplaying">if set to <c>true</c> [is displaying].</param>
        void UpdateData(bool isDisplaying);
    }
}
