using DI_Lite.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI_Lite
{
    internal class AutoConstructor<T, R>
        where R : class, T
    {
        public Func<IDependencyProvider, T> Creator { get; }

        public AutoConstructor()
        {
            Creator = InitializeCreator();
        }

        private Func<IDependencyProvider, T> InitializeCreator()
        {
            var type = typeof(R);
            var parameters = GetConstructorParameters();

            return (provider) =>
            {
                var args = GetConstructorArguments(parameters, provider);
                return (T)Activator.CreateInstance(type, args);
            };
        }

        private object[] GetConstructorArguments(IEnumerable<Type> parameters, IDependencyProvider provider)
        {
            object[] args;
            try
            {
                args = GetConstructorArgumentsUnsafe(parameters, provider);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
            return args;
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
            var longestConstructor = GetLongestConstructor();
            return longestConstructor
                .GetParameters()
                .Select(x => x.ParameterType);
        }

        private ConstructorInfo GetLongestConstructor()
        {
            var type = typeof(R);
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new DependencyHasNoConstructorException(type);
            if (constructors.Length > 1)
                throw new DependencyHasMultipleConstructorsException(type);
            return constructors.First();
        }
    }
}
