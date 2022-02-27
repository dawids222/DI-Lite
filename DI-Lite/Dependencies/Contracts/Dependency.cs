using System;

namespace DI_Lite.Dependencies.Contracts
{
    public abstract class Dependency<RefType> : IDependency
    {
        protected static Type ReferenceType => typeof(RefType);

        protected Func<IDependencyProvider, RefType> Creator { get; }

        protected Dependency(Func<IDependencyProvider, RefType> creator)
        {
            Creator = creator;
        }

        public abstract object Get(IDependencyProvider provider);
    }
}
