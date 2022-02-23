using DI_Lite.Dependencies.Contracts;
using DI_Lite.Exceptions;
using DI_Lite.Utils;
using System.Linq;

namespace DI_Lite.Dependencies
{
    internal class AutoConstructedScoped<ReferenceType, ConcreteType>
        : Scoped<ReferenceType>, IAutoConstructedDependency
        where ConcreteType : class, ReferenceType
    {
        private readonly AutoConstructor<ReferenceType, ConcreteType> _constructor;

        internal AutoConstructedScoped(AutoConstructor<ReferenceType, ConcreteType> constructor) : base(constructor.Creator)
        {
            _constructor = constructor;
        }

        public void ThrowIfIsNotConstructable(Container container)
        {
            var missingTypes = _constructor.Parameters.Where(p => !container.Contains(p));
            if (missingTypes.Any()) { throw new DependencyNotConstructableException(typeof(ConcreteType), missingTypes); }
        }
    }
}
