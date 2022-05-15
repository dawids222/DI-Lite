using DI_Lite.Dependencies;
using DI_Lite.Dependencies.Contracts;
using DI_Lite.Dependencies.Models;
using DI_Lite.Exceptions;
using DI_Lite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite
{
    public class Container : DependencyProvider
    {
        public IEnumerable<KeyValuePair<DependencyKey, IDependency>> Dependencies { get => _dependencies; }

        public Container() : base(new Dictionary<DependencyKey, IDependency>()) { }

        #region SINGLE
        public void Single<ReferenceType>(ReferenceType instance)
        {
            Single(null, instance);
        }

        public void Single<ReferenceType>(object tag, ReferenceType instance)
        {
            Single(tag, () => instance);
        }

        public void Single<ReferenceType>(Func<ReferenceType> creator)
        {
            Single(null, creator);
        }

        public void Single<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Singleton<ReferenceType>(ToProviderCreator(creator)));
        }

        public void Single<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            Single(null, creator);
        }

        public void Single<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Singleton<ReferenceType>(creator));
        }

        public void Single<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            Single<ConcreteType, ConcreteType>(tag);
        }

        public void Single<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            Single(tag, autoConstructor);
        }

        private void Single<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            AddDependency<ReferenceType>(tag, new AutoConstructedSingleton<ReferenceType, ConcreteType>(constructor));
        }

        public void TrySingle<ReferenceType>(ReferenceType instance)
        {
            TrySingle(null, instance);
        }

        public void TrySingle<ReferenceType>(object tag, ReferenceType instance)
        {
            TrySingle(tag, () => instance);
        }

        public void TrySingle<ReferenceType>(Func<ReferenceType> creator)
        {
            TrySingle(null, creator);
        }

        public void TrySingle<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            TryAddDependency<ReferenceType>(tag, new Singleton<ReferenceType>(ToProviderCreator(creator)));
        }

        public void TrySingle<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            TrySingle(null, creator);
        }

        public void TrySingle<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Singleton<ReferenceType>(creator));
        }

        public void TrySingle<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            TrySingle<ConcreteType, ConcreteType>(tag);
        }

        public void TrySingle<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            TrySingle(tag, autoConstructor);
        }

        private void TrySingle<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            TryAddDependency<ReferenceType>(tag, new AutoConstructedSingleton<ReferenceType, ConcreteType>(constructor));
        }

        public void ForceSingle<ReferenceType>(ReferenceType instance)
        {
            ForceSingle(null, instance);
        }

        public void ForceSingle<ReferenceType>(object tag, ReferenceType instance)
        {
            ForceSingle(tag, () => instance);
        }

        public void ForceSingle<ReferenceType>(Func<ReferenceType> creator)
        {
            ForceSingle(null, creator);
        }

        public void ForceSingle<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            ForceAddDependency<ReferenceType>(tag, new Singleton<ReferenceType>(ToProviderCreator(creator)));
        }

        public void ForceSingle<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            ForceSingle(null, creator);
        }

        public void ForceSingle<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Singleton<ReferenceType>(creator));
        }

        public void ForceSingle<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            ForceSingle<ConcreteType, ConcreteType>(tag);
        }

        public void ForceSingle<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            ForceSingle(tag, autoConstructor);
        }

        private void ForceSingle<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            ForceAddDependency<ReferenceType>(tag, new AutoConstructedSingleton<ReferenceType, ConcreteType>(constructor));
        }
        #endregion
        #region FACTORY
        public void Factory<ReferenceType>(Func<ReferenceType> creator)
        {
            Factory(null, creator);
        }

        public void Factory<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Factory<ReferenceType>(ToProviderCreator(creator)));
        }

        public void Factory<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            Factory(null, creator);
        }

        public void Factory<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Factory<ReferenceType>(creator));
        }

        public void Factory<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            Factory<ConcreteType, ConcreteType>(tag);
        }

        public void Factory<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            Factory(tag, autoConstructor);
        }
        private void Factory<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            AddDependency<ReferenceType>(tag, new AutoConstructedFactory<ReferenceType, ConcreteType>(constructor));
        }

        public void TryFactory<ReferenceType>(Func<ReferenceType> creator)
        {
            TryFactory(null, creator);
        }

        public void TryFactory<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            TryAddDependency<ReferenceType>(tag, new Factory<ReferenceType>(ToProviderCreator(creator)));
        }

        public void TryFactory<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            TryFactory(null, creator);
        }

        public void TryFactory<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Factory<ReferenceType>(creator));
        }

        public void TryFactory<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            TryFactory<ConcreteType, ConcreteType>(tag);
        }

        public void TryFactory<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            TryFactory(tag, autoConstructor);
        }

        private void TryFactory<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            TryAddDependency<ReferenceType>(tag, new AutoConstructedFactory<ReferenceType, ConcreteType>(constructor));
        }

        public void ForceFactory<ReferenceType>(Func<ReferenceType> creator)
        {
            ForceFactory(null, creator);
        }

        public void ForceFactory<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            ForceAddDependency<ReferenceType>(tag, new Factory<ReferenceType>(ToProviderCreator(creator)));
        }

        public void ForceFactory<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            ForceFactory(null, creator);
        }

        public void ForceFactory<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Factory<ReferenceType>(creator));
        }

        public void ForceFactory<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            ForceFactory<ConcreteType, ConcreteType>(tag);
        }

        public void ForceFactory<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            ForceFactory(tag, autoConstructor);
        }

        private void ForceFactory<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            ForceAddDependency<ReferenceType>(tag, new AutoConstructedFactory<ReferenceType, ConcreteType>(constructor));
        }
        #endregion
        #region SCOPED
        public void Scoped<ReferenceType>(Func<ReferenceType> creator)
        {
            Scoped(null, creator);
        }

        public void Scoped<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Scoped<ReferenceType>(ToProviderCreator(creator)));
        }

        public void Scoped<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            Scoped(null, creator);
        }

        public void Scoped<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Scoped<ReferenceType>(creator));
        }

        public void Scoped<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            Scoped<ConcreteType, ConcreteType>(tag);
        }

        public void Scoped<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            Scoped(tag, autoConstructor);
        }

        private void Scoped<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            AddDependency<ReferenceType>(tag, new AutoConstructedScoped<ReferenceType, ConcreteType>(constructor));
        }

        public void TryScoped<ReferenceType>(Func<ReferenceType> creator)
        {
            TryScoped(null, creator);
        }

        public void TryScoped<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            TryAddDependency<ReferenceType>(tag, new Scoped<ReferenceType>(ToProviderCreator(creator)));
        }

        public void TryScoped<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            TryScoped(null, creator);
        }

        public void TryScoped<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            TryAddDependency<ReferenceType>(tag, new Scoped<ReferenceType>(creator));
        }

        public void TryScoped<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            TryScoped<ConcreteType, ConcreteType>(tag);
        }

        public void TryScoped<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            TryScoped(tag, autoConstructor);
        }

        private void TryScoped<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            TryAddDependency<ReferenceType>(tag, new AutoConstructedScoped<ReferenceType, ConcreteType>(constructor));
        }

        public void ForceScoped<ReferenceType>(Func<ReferenceType> creator)
        {
            ForceScoped(null, creator);
        }

        public void ForceScoped<ReferenceType>(object tag, Func<ReferenceType> creator)
        {
            ForceAddDependency<ReferenceType>(tag, new Scoped<ReferenceType>(ToProviderCreator(creator)));
        }

        public void ForceScoped<ReferenceType>(Func<IDependencyProvider, ReferenceType> creator)
        {
            ForceScoped(null, creator);
        }

        public void ForceScoped<ReferenceType>(object tag, Func<IDependencyProvider, ReferenceType> creator)
        {
            AddDependency<ReferenceType>(tag, new Scoped<ReferenceType>(creator));
        }

        public void ForceScoped<ConcreteType>(object tag = null)
            where ConcreteType : class
        {
            ForceScoped<ConcreteType, ConcreteType>(tag);
        }

        public void ForceScoped<ReferenceType, ConcreteType>(object tag = null)
            where ConcreteType : class, ReferenceType
        {
            var autoConstructor = new AutoConstructor<ReferenceType, ConcreteType>();
            ForceScoped(tag, autoConstructor);
        }

        private void ForceScoped<ReferenceType, ConcreteType>(object tag, AutoConstructor<ReferenceType, ConcreteType> constructor)
            where ConcreteType : class, ReferenceType
        {
            ForceAddDependency<ReferenceType>(tag, new AutoConstructedScoped<ReferenceType, ConcreteType>(constructor));
        }
        #endregion
        #region DEPENDENCY
        private void AddDependency<ReferenceType>(object tag, IDependency dependency)
        {
            var key = new DependencyKey(typeof(ReferenceType), tag);
            try
            {
                _dependencies.Add(key, dependency);
            }
            catch (ArgumentException)
            {
                throw new DependencyAlreadyRegisteredException(key);
            }
        }

        private void TryAddDependency<ReferenceType>(object tag, IDependency dependency)
        {
            try { AddDependency<ReferenceType>(tag, dependency); }
            catch (Exception) { }
        }

        private void ForceAddDependency<ReferenceType>(object tag, IDependency dependency)
        {
            var key = new DependencyKey(typeof(ReferenceType), tag);
            if (_dependencies.ContainsKey(key))
            {
                _dependencies.Remove(key);
            }
            _dependencies.Add(key, dependency);
        }
        #endregion
        #region REMOVE
        public void Remove<ReferenceType>()
        {
            var type = typeof(ReferenceType);
            Remove(key => key.Type == type);
        }

        public void Remove(object tag)
        {
            Remove(key => key.Tag == tag);
        }

        public void Remove<ReferenceType>(object tag)
        {
            var type = typeof(ReferenceType);
            Remove(key => key.Type == type && key.Tag == tag);
        }

        public void Remove(Func<DependencyKey, bool> predicate)
        {
            var keys = _dependencies.Keys.Where(predicate);
            Remove(keys);
        }

        public void Remove(IEnumerable<DependencyKey> keys)
        {
            foreach (var key in keys.ToList())
                Remove(key);
        }

        public void Remove(DependencyKey key)
        {
            _dependencies.Remove(key);
        }
        #endregion
        #region SCOPE
        public ScopedContainer CreateScope()
        {
            var dependencies = _dependencies
                .Select(ReferenceTypeToScopeDependency)
                .ToDictionary(x => x.Key, x => x.Value);
            return new ScopedContainer(dependencies, this);
        }

        private KeyValuePair<DependencyKey, IDependency> ReferenceTypeToScopeDependency(KeyValuePair<DependencyKey, IDependency> dependency)
        {
            if (dependency.Value is IScopedDependency scopedDependency)
            {
                return new KeyValuePair<DependencyKey, IDependency>(dependency.Key, scopedDependency.ToScopedSingleton());
            }
            return dependency;
        }
        #endregion
        #region CONSTRUCTABILITY
        public bool IsConstructable => _dependencies.Values
            .OfType<IAutoConstructedDependency>()
            .All(d => d.IsConstructable(this));

        public ContainerConstructabilityReport GetConstructabilityReport()
        {
            var reports = _dependencies.Values
                .OfType<IAutoConstructedDependency>()
                .Select(d => d.GetConstructabilityReport(this));
            return new ContainerConstructabilityReport(reports);
        }

        public void ThrowIfNotConstructable()
        {
            var failedReports = GetConstructabilityReport().FailedConstructabilityReports;
            if (failedReports.Any()) { throw new ContainerNotConstructableException(failedReports); }
        }
        #endregion
        #region UTIL
        internal void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        private static Func<IDependencyProvider, ReferenceType> ToProviderCreator<ReferenceType>(Func<ReferenceType> creator)
        {
            return new Func<IDependencyProvider, ReferenceType>(provider => creator());
        }
        #endregion
    }
}
