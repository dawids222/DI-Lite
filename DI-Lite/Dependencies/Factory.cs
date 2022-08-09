using LibLite.DI.Lite.Dependencies.Contracts;
using LibLite.DI.Lite.Enums;
using System;

namespace LibLite.DI.Lite.Dependencies
{
    public class Factory<ReferenceType> : Dependency<ReferenceType>
    {
        public override DependencyType DependencyType => DependencyType.FACTORY;

        public Factory(Func<IDependencyProvider, ReferenceType> creator) : base(creator) { }

        public override object Get(IDependencyProvider provider)
        {
            return Creator(provider);
        }
    }
}
