using DI_Lite.Dependencies.Contracts;
using DI_Lite.Enums;
using System;

namespace DI_Lite.Dependencies
{
    public class Singleton<ReferenceType> : Dependency<ReferenceType>
    {
        public override DependencyType DependencyType => DependencyType.SINGLETON;

        private ReferenceType Instance { get; set; }
        private bool IsInitialized { get; set; } = false;

        public Singleton(Func<IDependencyProvider, ReferenceType> creator) : base(creator) { }

        public override object Get(IDependencyProvider provider)
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
