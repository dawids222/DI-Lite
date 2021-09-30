using System;

namespace DI_Lite.Dependencies
{
    public class Factory<T> : IDependency
    {
        public Func<IDependencyProvider, T> Creator { get; }

        public Factory(Func<IDependencyProvider, T> creator)
        {
            Creator = creator;
        }

        public object Get(IDependencyProvider provider)
        {
            return Creator(provider);
        }
    }
}
