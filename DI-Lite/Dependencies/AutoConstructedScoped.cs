using DI_Lite.Dependencies.Contracts;
using DI_Lite.Utils;

namespace DI_Lite.Dependencies
{
    internal class AutoConstructedScoped<ReferenceType, ConcreteType>
        : AutoConstructedDependency<ReferenceType, ConcreteType>, IScopedDependency
        where ConcreteType : class, ReferenceType
    {
        internal AutoConstructedScoped(AutoConstructor<ReferenceType, ConcreteType> constructor) : base(constructor) { }

        protected override Dependency<ReferenceType> CreateDependency()
            => new Scoped<ReferenceType>(Creator);

        public IDependency ToScopedSingleton()
            => AsScopedDependency().ToScopedSingleton();

        private IScopedDependency AsScopedDependency()
            => (IScopedDependency)_dependency;
    }
}
