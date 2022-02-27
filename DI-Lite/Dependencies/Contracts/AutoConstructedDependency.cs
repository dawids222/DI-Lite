using DI_Lite.Dependencies.Models;
using DI_Lite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite.Dependencies.Contracts
{
    internal abstract class AutoConstructedDependency<RefType, ConType>
        : Dependency<RefType>, IAutoConstructedDependency
        where ConType : class, RefType
    {
        internal static Type ConcreteType => typeof(ConType);

        protected readonly AutoConstructor<RefType, ConType> _constructor;
        protected readonly Dependency<RefType> _dependency;

        internal AutoConstructedDependency(AutoConstructor<RefType, ConType> constructor) : base(constructor.Creator)
        {
            _constructor = constructor;
            _dependency = CreateDependency();
        }

        protected abstract Dependency<RefType> CreateDependency();

        public override object Get(IDependencyProvider provider)
            => _dependency.Get(provider);

        public ConstructabilityReport GetConstructabilityReport(Container container)
            => new(typeof(RefType), typeof(ConType), GetMissingDependencies(container));

        public bool IsConstructable(Container container)
            => !GetMissingDependencies(container).Any();

        private IEnumerable<Type> GetMissingDependencies(Container container)
            => _constructor.Parameters.Where(p => !container.Contains(p));
    }
}
