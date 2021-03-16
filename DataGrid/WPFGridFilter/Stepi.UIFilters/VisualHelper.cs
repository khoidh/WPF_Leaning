using System.Windows;
using System.Windows.Media;

namespace Stepi.UIFilters
{
    public static class VisualHelper
    {
        /// <summary>
        /// Gets the parent of a given dependency object
        /// </summary>
        /// <typeparam name="T">Type of the parent</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>returns first parent of the given type</returns>
        public static T GetParent<T>(DependencyObject source)
                where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(source);
            while (parent != null && !typeof(T).IsAssignableFrom(parent.GetType()))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return (T)parent;
        }
    }
}
