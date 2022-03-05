using DI_Lite.Dependencies.Contracts;
using DI_Lite.Exceptions;
using System;
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
            return dependency.Get(this);
        }
    }
}
