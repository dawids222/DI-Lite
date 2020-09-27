using System;

namespace DI_Lite.Exceptions
{
    public class DependencyNotRegisteredException : Exception
    {
        private const string ERROR_MESSAGE = "Requested dependency type does not exist. Try registering it first";

        public DependencyNotRegisteredException() : base(ERROR_MESSAGE)
        {

        }
    }
}
