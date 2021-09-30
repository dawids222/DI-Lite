using System;

namespace DI_Lite.Dependencies
{
    public class Singleton<T> : IDependency
    {
        protected Func<IDependencyProvider, T> Creator { get; set; }
        private T Instance { get; set; }
        private bool IsInitialized { get; set; } = false;

        protected Singleton() { }

        public Singleton(Func<IDependencyProvider, T> creator)
        {
            Creator = creator;
        }

        public object Get(IDependencyProvider provider)
        {
            if (!IsInitialized) { Initialize(provider); }
            return Instance;
        }

        private void Initialize(IDependencyProvider provider)
        {
            Instance = Creator(provider);
            IsInitialized = true;
        }
    }
}
