using LibLite.DI.Lite.Dependencies.Models;
using LibLite.DI.Lite.Enums;
using LibLite.DI.Lite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibLite.DI.Lite.Dependencies.Contracts
{
    internal abstract class AutoConstructedDependency<TReference, TConcrete>
        : Dependency<TReference>, IAutoConstructedDependency
        where TConcrete : class, TReference
    {
        public override DependencyType DependencyType => _dependency.DependencyType;

        internal static Type ConcreteType => typeof(TConcrete);

        protected readonly AutoConstructor<TReference, TConcrete> _constructor;
        protected readonly Dependency<TReference> _dependency;

        internal AutoConstructedDependency(AutoConstructor<TReference, TConcrete> constructor) : base(constructor.Creator)
        {
            _constructor = constructor;
            _dependency = CreateDependency();
        }

        protected abstract Dependency<TReference> CreateDependency();

        public override object Get(IDependencyProvider provider)
            => _dependency.Get(provider);

        public DependencyConstructabilityReport GetConstructabilityReport(Container container)
            => new(ReferenceType, ConcreteType, GetMissingDependencies(container));

        public bool IsConstructable(Container container)
            => !GetMissingDependencies(container).Any();

        private IEnumerable<DependencyKey> GetMissingDependencies(Container container)
            => _constructor.Parameters.Where(x => !container.Contains(x.Type, x.Tag)).ToList();
    }
}
