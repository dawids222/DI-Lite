using System;

namespace DI_Lite.Dependencies
{
    public class Factory<T> : IDependency
    {
        public Func<T> Creator { get; }

        public Factory(Func<T> creator)
        {
            Creator = creator;
        }

        public object Get()
        {
            return Creator();
        }
    }
}
