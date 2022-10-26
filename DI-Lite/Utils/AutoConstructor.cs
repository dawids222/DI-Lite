using LibLite.DI.Lite.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LibLite.DI.Lite.Utils
{
    internal class AutoConstructor<ReferenceType, ConcreteType>
        where ConcreteType : class, ReferenceType
    {
        public Func<IDependencyProvider, ReferenceType> Creator { get; }
        public IEnumerable<DependencyKey> Parameters { get; }

        public AutoConstructor()
        {
            Parameters = GetConstructorParameters();
            Creator = InitializeCreator();
        }

        private Func<IDependencyProvider, ReferenceType> InitializeCreator()
        {
            var concreteType = typeof(ConcreteType);
            return (provider) =>
            {
                var args = GetConstructorArguments(Parameters, provider);
                return (ReferenceType)Activator.CreateInstance(concreteType, args);
            };
        }

        private static object[] GetConstructorArguments(IEnumerable<DependencyKey> parameters, IDependencyProvider provider)
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

        private static object[] GetConstructorArgumentsUnsafe(IEnumerable<DependencyKey> parameters, IDependencyProvider provider)
        {
            return parameters
                .Select(parameter => typeof(IDependencyProvider)
                    .GetMethods()
                    .First(method =>
                        method.Name == nameof(IDependencyProvider.Get) &&
                        method.IsGenericMethod)
                    .MakeGenericMethod(parameter.Type)
                    .Invoke(provider, new object[] { parameter.Tag }))
                .ToArray();
        }

        private static IEnumerable<DependencyKey> GetConstructorParameters()
        {
            var constructor = GetConstructor();
            return constructor
                .GetParameters()
                .Select(x => new DependencyKey(x))
                .ToList();
        }

        private static ConstructorInfo GetConstructor()
        {
            var concreteType = typeof(ConcreteType);
            var constructors = new AutoConstructorsInfo(concreteType);
            return constructors switch
            {
                { Total: 0 } => throw new DependencyHasNoConstructorException(concreteType),
                { Total: > 1, Decorated: 0 } => throw new DependencyHasMultipleConstructorsException(concreteType),
                { Total: > 1, Decorated: > 1 } => throw new DependencyHasMultipleUseConstructorAttributesException(concreteType),
                { Total: > 1, Decorated: 1 } => constructors.FirstDecorated(),
                _ => constructors.First(),
            };
        }
    }
}
