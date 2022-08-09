using LibLite.DI.Lite.Dependencies.Contracts;
using LibLite.DI.Lite.Enums;
using LibLite.DI.Lite.Exceptions;
using System;

namespace LibLite.DI.Lite.Dependencies
{
    public class Scoped<ReferenceType> : Dependency<ReferenceType>, IScopedDependency
    {
        public override DependencyType DependencyType => DependencyType.SCOPED;

        public Scoped(Func<IDependencyProvider, ReferenceType> creator) : base(creator) { }

        public override object Get(IDependencyProvider provider)
        {
            throw new DependencyRetrievalRequiresScopeException();
        }

        public IDependency ToScopedSingleton()
        {
            return new ScopedSingleton<ReferenceType>(Creator);
        }
    }
}
