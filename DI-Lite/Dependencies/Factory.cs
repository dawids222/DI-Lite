using DI_Lite.Dependencies.Contracts;
using DI_Lite.Enums;
using System;

namespace DI_Lite.Dependencies
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
