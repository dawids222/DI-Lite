using DI_Lite.Dependencies.Contracts;
using System;
using System.Collections.Generic;

namespace DI_Lite
{
    public class ScopedContainer : DependencyProvider
    {
        private readonly Container _parent;

        internal ScopedContainer(
            Dictionary<DependencyKey, IDependency> dependencies,
            Container parent)
            : base(dependencies)
        {
            _parent = parent;
        }

        protected override void OnGetIDisposable(IDisposable disposable, IDependency dependency)
        {
            var isSingleton = dependency.DependencyType == Enums.DependencyType.SINGLETON;
            if (isSingleton) { _parent.AddDisposable(disposable); }
            else { base.OnGetIDisposable(disposable, dependency); }
        }
    }
}
