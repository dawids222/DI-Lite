using System;

namespace DI_Lite.Exceptions
{
    public class DependencyAlreadyRegisteredException : Exception
    {
        public DependencyKey Key { get; }

        public DependencyAlreadyRegisteredException(DependencyKey key)
            : base($"Dependency of type {key.Type.FullName} and tag {key.Tag} does already exist.")
        {
            Key = key;
        }
    }
}
