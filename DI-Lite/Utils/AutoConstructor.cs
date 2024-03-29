﻿using LibLite.DI.Lite.Exceptions;
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
        public IEnumerable<DependencyKey> Parameters { get; private set; }

        public AutoConstructor()
        {
            Creator = InitializeCreator();
        }

        private Func<IDependencyProvider, ReferenceType> InitializeCreator()
        {
            var concreteType = typeof(ConcreteType);
            Parameters = GetConstructorParameters();

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
                .Select(x => new DependencyKey(x));
        }

        private static ConstructorInfo GetConstructor()
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
