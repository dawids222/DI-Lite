using DI_Lite.Dependencies;
using DI_Lite.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite
{
    public class Container : IDependencyProvider
    {
        private Dictionary<DependencyKey, IDependency> dependencies = new Dictionary<DependencyKey, IDependency>();
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
            AddDependency<T>(tag, new Singleton<T>(ToProviderCreator(creator)));
        }

        private void Single<T>(object tag, Func<IDependencyProvider, T> creator)
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
            var autoConstructor = new AutoConstructor<T, R>();
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
            TryAddDependency<T>(tag, new Singleton<T>(ToProviderCreator(creator)));
        }

        private void TrySingle<T>(object tag, Func<IDependencyProvider, T> creator)
        {
            AddDependency<T>(tag, new Singleton<T>(creator));
        }

        public void TrySingle<T>(object tag = null)
            where T : class
        {
            TrySingle<T, T>(tag);
        }

        public void TrySingle<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>();
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
            ForceAddDependency<T>(tag, new Singleton<T>(ToProviderCreator(creator)));
        }

        private void ForceSingle<T>(object tag, Func<IDependencyProvider, T> creator)
        {
            AddDependency<T>(tag, new Singleton<T>(creator));
        }

        public void ForceSingle<T>(object tag = null)
            where T : class
        {
            ForceSingle<T, T>(tag);
        }

        public void ForceSingle<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>();
            ForceSingle<T>(tag, autoConstructor.Creator);
        }

        public void Factory<T>(Func<T> creator)
        {
            Factory(null, creator);
        }

        public void Factory<T>(object tag, Func<T> creator)
        {
            AddDependency<T>(tag, new Factory<T>(ToProviderCreator(creator)));
        }

        private void Factory<T>(object tag, Func<IDependencyProvider, T> creator)
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
            var autoConstructor = new AutoConstructor<T, R>();
            Factory<T>(tag, autoConstructor.Creator);
        }

        public void TryFactory<T>(Func<T> creator)
        {
            TryFactory(null, creator);
        }

        public void TryFactory<T>(object tag, Func<T> creator)
        {
            TryAddDependency<T>(tag, new Factory<T>(ToProviderCreator(creator)));
        }

        private void TryFactory<T>(object tag, Func<IDependencyProvider, T> creator)
        {
            AddDependency<T>(tag, new Factory<T>(creator));
        }

        public void TryFactory<T>(object tag = null)
            where T : class
        {
            TryFactory<T, T>(tag);
        }

        public void TryFactory<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>();
            TryFactory<T>(tag, autoConstructor.Creator);
        }

        public void ForceFactory<T>(Func<T> creator)
        {
            ForceFactory(null, creator);
        }

        public void ForceFactory<T>(object tag, Func<T> creator)
        {
            ForceAddDependency<T>(tag, new Factory<T>(ToProviderCreator(creator)));
        }

        private void ForceFactory<T>(object tag, Func<IDependencyProvider, T> creator)
        {
            AddDependency<T>(tag, new Factory<T>(creator));
        }

        public void ForceFactory<T>(object tag = null)
            where T : class
        {
            ForceFactory<T, T>(tag);
        }

        public void ForceFactory<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>();
            ForceFactory<T>(tag, autoConstructor.Creator);
        }

        public void Scoped<T>(Func<T> creator)
        {
            Scoped(null, creator);
        }

        public void Scoped<T>(object tag, Func<T> creator)
        {
            AddDependency<T>(tag, new Scoped<T>(ToProviderCreator(creator)));
        }

        private void Scoped<T>(object tag, Func<IDependencyProvider, T> creator)
        {
            AddDependency<T>(tag, new Scoped<T>(creator));
        }

        public void Scoped<T>(object tag = null)
            where T : class
        {
            Scoped<T, T>(tag);
        }

        public void Scoped<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>();
            Scoped<T>(tag, autoConstructor.Creator);
        }

        public void TryScoped<T>(Func<T> creator)
        {
            TryScoped(null, creator);
        }

        public void TryScoped<T>(object tag, Func<T> creator)
        {
            TryAddDependency<T>(tag, new Scoped<T>(ToProviderCreator(creator)));
        }

        private void TryScoped<T>(object tag, Func<IDependencyProvider, T> creator)
        {
            AddDependency<T>(tag, new Scoped<T>(creator));
        }

        public void TryScoped<T>(object tag = null)
            where T : class
        {
            TryScoped<T, T>(tag);
        }

        public void TryScoped<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>();
            TryScoped<T>(tag, autoConstructor.Creator);
        }

        public void ForceScoped<T>(Func<T> creator)
        {
            ForceScoped(null, creator);
        }

        public void ForceScoped<T>(object tag, Func<T> creator)
        {
            ForceAddDependency<T>(tag, new Scoped<T>(ToProviderCreator(creator)));
        }

        private void ForceScoped<T>(object tag, Func<IDependencyProvider, T> creator)
        {
            AddDependency<T>(tag, new Scoped<T>(creator));
        }

        public void ForceScoped<T>(object tag = null)
            where T : class
        {
            ForceScoped<T, T>(tag);
        }

        public void ForceScoped<T, R>(object tag = null)
            where R : class, T
        {
            var autoConstructor = new AutoConstructor<T, R>();
            ForceScoped<T>(tag, autoConstructor.Creator);
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
            return (T)dependency.Get(this);
        }

        public ScopedContainer CreateScope()
        {
            var dependencies = this.dependencies
                .Select(ToScopeDependency)
                .ToDictionary(x => x.Key, x => x.Value);
            return new ScopedContainer(dependencies);
        }

        private KeyValuePair<DependencyKey, IDependency> ToScopeDependency(KeyValuePair<DependencyKey, IDependency> dependency)
        {
            if (dependency.Value is IScopedDependency scopedDependency)
            {
                return new KeyValuePair<DependencyKey, IDependency>(dependency.Key, scopedDependency.ToSingleton());
            }
            return dependency;
        }

        private Func<IDependencyProvider, T> ToProviderCreator<T>(Func<T> creator)
        {
            return new Func<IDependencyProvider, T>(provider => creator());
        }
    }
}
