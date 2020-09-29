using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI_Lite
{
    internal class AutoConstructor<T, R>
        where R : class, T
    {
        public Func<T> Creator { get; }
        private Container Container { get; }

        public AutoConstructor(Container container)
        {
            Container = container;
            Creator = InitializeCreator();
        }

        private Func<T> InitializeCreator()
        {
            return (() =>
            {
                var type = typeof(R);
                var args = GetConstructorArguments();
                return (T)Activator.CreateInstance(type, args);
            });
        }

        private object[] GetConstructorArguments()
        {
            object[] args;
            try
            {
                args = GetConstructorArgumentsUnsafe();
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
            return args;
        }

        private object[] GetConstructorArgumentsUnsafe()
        {
            return GetConstructorParameters()
                .Select(type => typeof(Container)
                .GetMethod("Get")
                .MakeGenericMethod(type)
                .Invoke(Container, new object[] { null })
            ).ToArray();
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
            return constructors
                .OrderBy(x => x.GetParameters().Length)
                .Last();
        }
    }
}
