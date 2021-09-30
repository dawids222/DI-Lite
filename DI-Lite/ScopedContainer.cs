using DI_Lite.Dependencies;
using DI_Lite.Exceptions;
using System.Collections.Generic;

namespace DI_Lite
{
    public class ScopedContainer : IDependencyProvider
    {
        private readonly Dictionary<DependencyKey, IDependency> _dependencies;

        internal ScopedContainer(Dictionary<DependencyKey, IDependency> dependencies)
        {
            _dependencies = dependencies;
        }

        public T Get<T>(object tag = null)
        {
            var key = new DependencyKey(typeof(T), tag);
            if (!_dependencies.ContainsKey(key))
            {
                throw new DependencyNotRegisteredException(key);
            }
            var dependency = _dependencies[key];
            return (T)dependency.Get(this);
        }
    }
}
