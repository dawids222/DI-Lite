using System;

namespace DI_Lite.Dependencies
{
    public class Singleton<T> : IDependency
    {
        protected Func<T> Creator { get; set; }
        private T Instance { get; set; }
        private bool IsInitialized { get; set; } = false;

        protected Singleton()
        {

        }

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
