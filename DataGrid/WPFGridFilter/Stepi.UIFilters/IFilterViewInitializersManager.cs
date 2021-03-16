using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Stepi.UIFilters
{
    public interface IFilterViewInitializersManager
    {
        //void Register(IFilterViewInitializer viewInitializer);
        //void Remove(IFilterViewInitializer viewInitializer);
        //bool Contains(IFilterViewInitializer viewInitializer);
        //IEnumerable<IFilterViewInitializer> Initializers { get; }

        IList<IFilterViewInitializer> Initializers { get; }

        IEnumerable<IFilterView> CreateView(PropertyInfo propertyInfo, ICollectionView collection);

    }
}
