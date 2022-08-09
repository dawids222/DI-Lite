using System;

namespace LibLite.DI.Lite.Exceptions
{
    public class DependencyHasMultipleConstructorsException : Exception
    {
        public Type Type { get; }

        public DependencyHasMultipleConstructorsException(Type type)
            : base($"Dependency of type {type.FullName} can not be automatically registered, because it contains multiple public constructors. Dependecy types created automatically must contain exactly 1 public constructor")
        {
            Type = type;
        }
    }
}
