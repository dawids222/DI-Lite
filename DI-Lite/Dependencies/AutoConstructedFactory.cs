using DI_Lite.Dependencies.Contracts;
using DI_Lite.Utils;

namespace DI_Lite.Dependencies
{
    internal class AutoConstructedFactory<ReferenceType, ConcreteType>
        : AutoConstructedDependency<ReferenceType, ConcreteType>
        where ConcreteType : class, ReferenceType
    {
        internal AutoConstructedFactory(AutoConstructor<ReferenceType, ConcreteType> constructor) : base(constructor) { }

        protected override Dependency<ReferenceType> CreateDependency()
            => new Factory<ReferenceType>(Creator);
    }
}
