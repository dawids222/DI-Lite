using System;

namespace DI_Lite.Dependencies
{
    public class Singleton<T> : IDependency
    {
        private Func<T> Creator { get; }
        private T Instance { get; set; }
        private bool IsInitialized { get; set; } = false;

        public Singleton(Func<T> creator)
        {
            Creator = creator;
        }

        public object Get()
        {
            if (!IsInitialized) { Initialize(); }
            return Instance;
        }

        private void Initialize()
        {
            Instance = Creator();
            IsInitialized = true;
        }
    }
}
