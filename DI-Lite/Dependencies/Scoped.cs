using DI_Lite.Dependencies.Contracts;
using DI_Lite.Exceptions;
using System;

namespace DI_Lite.Dependencies
{
    public class Scoped<ReferenceType> : IScopedDependency
    {
        protected Func<IDependencyProvider, ReferenceType> Creator { get; }

        public Scoped(Func<IDependencyProvider, ReferenceType> creator)
        {
            Creator = creator;
        }

        public object Get(IDependencyProvider provider)
        {
            throw new DependencyRetrievalRequiresScopeException();
        }

        public IDependency ToSingleton()
        {
            return new Singleton<ReferenceType>(Creator);
        }
    }
}
