using LibLite.DI.Lite.Dependencies.Contracts;
using LibLite.DI.Lite.Utils;

namespace LibLite.DI.Lite.Dependencies
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
