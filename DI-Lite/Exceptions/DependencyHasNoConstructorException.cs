using System;

namespace DI_Lite.Exceptions
{
    public class DependencyHasNoConstructorException : Exception
    {
        public Type Type { get; }

        public DependencyHasNoConstructorException(Type type)
            : base($"Dependency of type {type.FullName} can not be automatically registered, because it contains no public constructor. Dependecy types created automatically must contain exactly 1 public constructor")
        {
            Type = type;
        }
    }
}
