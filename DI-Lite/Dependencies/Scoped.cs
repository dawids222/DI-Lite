using DI_Lite.Dependencies.Contracts;
using DI_Lite.Exceptions;
using System;

namespace DI_Lite.Dependencies
{
    public class Scoped<ReferenceType> : Dependency<ReferenceType>, IScopedDependency
    {
        public Scoped(Func<IDependencyProvider, ReferenceType> creator) : base(creator) { }

        public override object Get(IDependencyProvider provider)
        {
            throw new DependencyRetrievalRequiresScopeException();
        }

        public IDependency ToSingleton()
        {
            return new Singleton<ReferenceType>(Creator);
        }
    }
}
