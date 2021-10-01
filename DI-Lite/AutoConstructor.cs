using DI_Lite.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI_Lite
{
    internal class AutoConstructor<ReferenceType, ConcreteType>
        where ConcreteType : class, ReferenceType
    {
        public Func<IDependencyProvider, ReferenceType> Creator { get; }

        public AutoConstructor()
        {
            Creator = InitializeCreator();
        }

        private Func<IDependencyProvider, ReferenceType> InitializeCreator()
        {
            var concreteType = typeof(ConcreteType);
            var parameters = GetConstructorParameters();

            return (provider) =>
            {
                var args = GetConstructorArguments(parameters, provider);
                return (ReferenceType)Activator.CreateInstance(concreteType, args);
            };
        }

        private object[] GetConstructorArguments(IEnumerable<Type> parameters, IDependencyProvider provider)
        {
            try
            {
                return GetConstructorArgumentsUnsafe(parameters, provider);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        private object[] GetConstructorArgumentsUnsafe(IEnumerable<Type> parameters, IDependencyProvider provider)
        {
            return parameters
                .Select(type => typeof(IDependencyProvider)
                    .GetMethod(nameof(IDependencyProvider.Get))
                    .MakeGenericMethod(type)
                    .Invoke(provider, new object[] { null }))
                .ToArray();
        }

        private IEnumerable<Type> GetConstructorParameters()
        {
            var constructor = GetConstructor();
            return constructor
                .GetParameters()
                .Select(x => x.ParameterType);
        }

        private ConstructorInfo GetConstructor()
        {
            var concreteType = typeof(ConcreteType);
            var constructors = concreteType.GetConstructors();
            if (constructors.Length == 0)
                throw new DependencyHasNoConstructorException(concreteType);
            if (constructors.Length > 1)
                throw new DependencyHasMultipleConstructorsException(concreteType);
            return constructors.First();
        }
    }
}
