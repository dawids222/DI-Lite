using System;

namespace DI_Lite.Dependencies
{
    public class Singleton<ReferenceType> : IDependency
    {
        protected Func<IDependencyProvider, ReferenceType> Creator { get; }
        private ReferenceType Instance { get; set; }
        private bool IsInitialized { get; set; } = false;

        public Singleton(Func<IDependencyProvider, ReferenceType> creator)
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
