using DI_Lite.Enums;
using System;

namespace DI_Lite.Dependencies
{
    public class ScopedSingleton<ReferenceType> : Singleton<ReferenceType>
    {
        public override DependencyType DependencyType => DependencyType.SCOPED;

        public ScopedSingleton(Func<IDependencyProvider, ReferenceType> creator)
            : base(creator) { }
    }
}
