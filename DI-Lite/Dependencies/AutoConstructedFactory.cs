using LibLite.DI.Lite.Dependencies.Contracts;
using LibLite.DI.Lite.Utils;

namespace LibLite.DI.Lite.Dependencies
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
