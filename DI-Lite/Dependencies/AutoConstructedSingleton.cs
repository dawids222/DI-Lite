using DI_Lite.Dependencies.Contracts;
using DI_Lite.Utils;

namespace DI_Lite.Dependencies
{
    internal class AutoConstructedSingleton<ReferenceType, ConcreteType>
        : AutoConstructedDependency<ReferenceType, ConcreteType>
        where ConcreteType : class, ReferenceType
    {
        internal AutoConstructedSingleton(AutoConstructor<ReferenceType, ConcreteType> constructor) : base(constructor) { }

        protected override Dependency<ReferenceType> CreateDependency()
            => new Singleton<ReferenceType>(Creator);
    }
}
