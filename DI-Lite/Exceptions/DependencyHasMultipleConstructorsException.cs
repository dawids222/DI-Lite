using System;

namespace DI_Lite.Exceptions
{
    public class DependencyHasMultipleConstructorsException : Exception
    {
        public Type Type { get; }

        public DependencyHasMultipleConstructorsException(Type type)
            : base($"Dependency of type {type.FullName} can not be registered, because it contains multiple public constructors. Dependecy types must contain only 1 public constructor")
        {
            Type = type;
        }
    }
}
