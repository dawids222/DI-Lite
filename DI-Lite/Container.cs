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

        public void Single<T>(T instance)
        {
            Single(null, instance);
        }

        public void Single<T>(object tag, T instance)
        {
            Single(tag, () => instance);
        }

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

        public void TrySingle<T>(T instance)
        {
            TrySingle(null, instance);
        }

        public void TrySingle<T>(object tag, T instance)
        {
            TrySingle(tag, () => instance);
        }

        public void TrySingle<T>(Func<T> creator)
        {
            TrySingle(null, creator);
        }

        public void TrySingle<T>(object tag, Func<T> creator)
        {
            TryAddDependency<T>(tag, new Singleton<T>(creator));
        }

        public void TrySingle<T>(object tag = null)
            where T : class
        {
            TrySingle<T, T>(tag);
        }

        public void TrySingle<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>(this);
            TrySingle<T>(tag, autoConstructor.Creator);
        }

        public void ForceSingle<T>(T instance)
        {
            ForceSingle(null, instance);
        }

        public void ForceSingle<T>(object tag, T instance)
        {
            ForceSingle(tag, () => instance);
        }

        public void ForceSingle<T>(Func<T> creator)
        {
            ForceSingle(null, creator);
        }

        public void ForceSingle<T>(object tag, Func<T> creator)
        {
            ForceAddDependency<T>(tag, new Singleton<T>(creator));
        }

        public void ForceSingle<T>(object tag = null)
            where T : class
        {
            ForceSingle<T, T>(tag);
        }

        public void ForceSingle<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>(this);
            ForceSingle<T>(tag, autoConstructor.Creator);
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

        public void TryFactory<T>(Func<T> creator)
        {
            TryFactory(null, creator);
        }

        public void TryFactory<T>(object tag, Func<T> creator)
        {
            TryAddDependency<T>(tag, new Factory<T>(creator));
        }

        public void TryFactory<T>(object tag = null)
            where T : class
        {
            TryFactory<T, T>(tag);
        }

        public void TryFactory<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>(this);
            TryFactory<T>(tag, autoConstructor.Creator);
        }

        public void ForceFactory<T>(Func<T> creator)
        {
            ForceFactory(null, creator);
        }

        public void ForceFactory<T>(object tag, Func<T> creator)
        {
            ForceAddDependency<T>(tag, new Factory<T>(creator));
        }

        public void ForceFactory<T>(object tag = null)
            where T : class
        {
            ForceFactory<T, T>(tag);
        }

        public void ForceFactory<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>(this);
            ForceFactory<T>(tag, autoConstructor.Creator);
        }

        private void AddDependency<T>(object tag, IDependency dependency)
        {
            var key = new DependencyKey(typeof(T), tag);
            try
            {
                dependencies.Add(key, dependency);
            }
            catch (ArgumentException)
            {
                throw new DependencyAlreadyRegisteredException(key);
            }
        }

        private void TryAddDependency<T>(object tag, IDependency dependency)
        {
            try { AddDependency<T>(tag, dependency); }
            catch (Exception) { }
        }

        private void ForceAddDependency<T>(object tag, IDependency dependency)
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
                throw new DependencyNotRegisteredException(key);
            }
            var dependency = dependencies[key];
            return (T)dependency.Get();
        }
    }
}
