using System;

namespace DI_Lite.Dependencies
{
    public class Factory<ReferenceType> : IDependency
    {
        protected Func<IDependencyProvider, ReferenceType> Creator { get; }

        public Factory(Func<IDependencyProvider, ReferenceType> creator)
        {
            Creator = creator;
        }

        public object Get(IDependencyProvider provider)
        {
            return Creator(provider);
        }
    }
}
