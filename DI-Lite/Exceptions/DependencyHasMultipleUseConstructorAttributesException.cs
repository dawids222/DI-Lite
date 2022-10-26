using LibLite.DI.Lite.Attributes;
using System;

namespace LibLite.DI.Lite.Exceptions
{
    public class DependencyHasMultipleUseConstructorAttributesException : Exception
    {
        public Type Type { get; }

        public DependencyHasMultipleUseConstructorAttributesException(Type type)
            : base($"Dependency of type {type.FullName} can not be automatically registered, because it contains multiple public constructors decorated with '${nameof(UseConstructorAttribute)}'. At most 1 public constructor can be decorated with it.")
        {
            Type = type;
        }
    }
}
