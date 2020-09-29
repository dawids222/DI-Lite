using System;

namespace DI_Lite.Exceptions
{
    public class DependencyNotRegisteredException : Exception
    {
        public DependencyKey Key { get; }

        public DependencyNotRegisteredException(DependencyKey key)
            : base($"Requested dependency of type {key.Type.FullName} and tag {key.Tag} does not exist. Try registering it first")
        {
            Key = key;
        }
    }
}
