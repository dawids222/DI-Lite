using LibLite.DI.Lite.Enums;
using System;

namespace LibLite.DI.Lite.Dependencies
{
    public class ScopedSingleton<ReferenceType> : Singleton<ReferenceType>
    {
        public override DependencyType DependencyType => DependencyType.SCOPED;

        public ScopedSingleton(Func<IDependencyProvider, ReferenceType> creator)
            : base(creator) { }
    }
}
