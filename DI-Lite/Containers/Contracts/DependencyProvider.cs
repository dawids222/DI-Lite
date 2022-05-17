using DI_Lite.Dependencies.Contracts;
using DI_Lite.Exceptions;
using System;
using System.Collections.Generic;

namespace DI_Lite
{
    public abstract class DependencyProvider : IDependencyProvider, IDisposable
    {
        protected readonly IDictionary<DependencyKey, IDependency> _dependencies;
        protected readonly ICollection<IDisposable> _disposables;

        internal DependencyProvider(IDictionary<DependencyKey, IDependency> dependencies)
        {
            _dependencies = dependencies;

            var equalityComparer = ReferenceEqualityComparer.Instance;
            _disposables = new HashSet<IDisposable>(equalityComparer);
        }

        public ReferenceType Get<ReferenceType>(object tag = null)
        {
            return (ReferenceType)Get(typeof(ReferenceType), tag);
        }

        public object Get(Type referenceType, object tag = null)
        {
            var key = new DependencyKey(referenceType, tag);
            if (!_dependencies.ContainsKey(key))
            {
                throw new DependencyNotRegisteredException(key);
            }
            var dependency = _dependencies[key];
            var result = dependency.Get(this);

            if (result is IDisposable disposable and not IDependencyProvider)
            {
                OnGetIDisposable(disposable, dependency);
            }

            return result;
        }

        protected virtual void OnGetIDisposable(IDisposable disposable, IDependency dependency)
        {
            _disposables.Add(disposable);
        }

        public bool Contains<T>(object tag = null)
        {
            return Contains(typeof(T), tag);
        }

        public bool Contains(Type referenceType, object tag = null)
        {
            var key = new DependencyKey(referenceType, tag);
            return _dependencies.ContainsKey(key);
        }

        public void Dispose()
        {
            foreach (var dependency in _disposables)
            {
                dependency.Dispose();
            }
        }
    }
}
