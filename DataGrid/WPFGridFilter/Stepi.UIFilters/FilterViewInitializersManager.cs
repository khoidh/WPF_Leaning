using System;
using System.Collections.Generic;
using System.Reflection;
using Stepi.UIFilters.Initializers;
using System.ComponentModel;

namespace Stepi.UIFilters
{
    public sealed class FilterViewInitializersManager : IFilterViewInitializersManager
    {
        private List<IFilterViewInitializer> _initializers;

        public FilterViewInitializersManager()
        {
            _initializers = new List<IFilterViewInitializer>();

            _initializers.Add(new EqualFilterInitializer());
            _initializers.Add(new GreterOrEqualFilterInitializer());
            _initializers.Add(new LessOrEqualFilterInitializer());
            _initializers.Add(new RangeFilterInitializer());
            _initializers.Add(new StringFilterInitializer());
        }

        public FilterViewInitializersManager(IEnumerable<IFilterViewInitializer> initializers)
        {
            if (initializers == null)
            {
                throw new ArgumentNullException("initializers");
            }
            _initializers = new List<IFilterViewInitializer>(initializers);
        }

        #region IFilterViewInitializersManager Members

        public IList<IFilterViewInitializer> Initializers
        {
            get { return _initializers; }
        }

        public IEnumerable<IFilterView> CreateView(PropertyInfo propertyInfo, ICollectionView collection)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            foreach (IFilterViewInitializer initializer in _initializers)
            {
                if (initializer.IsApplying(propertyInfo))
                {
                    yield return initializer.GetFilterView(propertyInfo, collection);
                }
            }
        }

        //public void Register(IFilterViewInitializer viewInitializer)
        //{
        //    if (viewInitializer == null)
        //    {
        //        throw new ArgumentNullException("viewInitializer");
        //    }

        //    if (!_initializers.Contains(viewInitializer))
        //    {
        //        _initializers.Add(viewInitializer);
        //    }
        //}

        //public void Remove(IFilterViewInitializer viewInitializer)
        //{
        //    _initializers.Remove(viewInitializer);
        //}

        //public bool Contains(IFilterViewInitializer viewInitializer)
        //{
        //    return _initializers.Contains(viewInitializer);
        //}

        //public IEnumerable<IFilterViewInitializer> Initializers
        //{
        //    get
        //    {
        //        foreach (IFilterViewInitializer initializer in _initializers)
        //        {
        //            yield return initializer;
        //        }
        //    }
        //}

        #endregion
    }
}
