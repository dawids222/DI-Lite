using DI_Lite.Dependencies;
using DI_Lite.Exceptions;
using System;
using System.Collections.Generic;

namespace DI_Lite
{
    public class Container
    {
        private Dictionary<DependencyKey, IDependency> dependencies { get; } = new Dictionary<DependencyKey, IDependency>();
        public IEnumerable<KeyValuePair<DependencyKey, IDependency>> Dependencies { get => dependencies; }

        public void Single<T>(Func<T> creator)
        {
            Single(null, creator);
        }

        public void Single<T>(object tag, Func<T> creator)
        {
            AddDependency<T>(tag, new Singleton<T>(creator));
        }

        public void Single<T>(object tag = null)
            where T : class
        {
            Single<T, T>(tag);
        }

        public void Single<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>(this);
            Single<T>(tag, autoConstructor.Creator);
        }

        public void Factory<T>(Func<T> creator)
        {
            Factory(null, creator);
        }

        public void Factory<T>(object tag, Func<T> creator)
        {
            AddDependency<T>(tag, new Factory<T>(creator));
        }

        public void Factory<T>(object tag = null)
            where T : class
        {
            Factory<T, T>(tag);
        }

        public void Factory<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>(this);
            Factory<T>(tag, autoConstructor.Creator);
        }

        private void AddDependency<T>(object tag, IDependency dependency)
        {
            var key = new DependencyKey(typeof(T), tag);
            if (dependencies.ContainsKey(key))
            {
                dependencies.Remove(key);
            }
            dependencies.Add(key, dependency);
        }

        public T Get<T>(object tag = null)
        {
            var key = new DependencyKey(typeof(T), tag);
            if (!dependencies.ContainsKey(key))
            {
                throw new DependencyNotRegisteredException();
            }
            var dependency = dependencies[key];
            return (T)dependency.Get();
        }
    }
}
