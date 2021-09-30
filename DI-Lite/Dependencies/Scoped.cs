using DI_Lite.Exceptions;
using System;

namespace DI_Lite.Dependencies
{
    public class Scoped<T> : IScopedDependency
    {
        protected Func<IDependencyProvider, T> Creator { get; set; }

        public Scoped(Func<IDependencyProvider, T> creator)
        {
            Creator = creator;
        }

        public object Get(IDependencyProvider provider)
        {
            throw new DependencyRetrievalRequireScopeException();
        }

        public IDependency ToSingleton()
        {
            return new Singleton<T>(Creator);
        }
    }
}
