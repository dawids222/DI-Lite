﻿using DI_Lite.Dependencies;
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

        public void Single<T, R>(object tag = null)
            where R : class, T
        {
            AddDependency<T>(tag, new AutoConstructedSingleton<T, R>(this));
        }

        public void Factory<T>(Func<T> creator)
        {
            Factory(null, creator);
        }

        public void Factory<T>(object tag, Func<T> creator)
        {
            AddDependency<T>(tag, new Factory<T>(creator));
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
