using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite.Exceptions
{
    public class ContainerNotConstructableException : Exception
    {
        public IEnumerable<DependencyNotConstructableException> Exceptions { get; }

        public ContainerNotConstructableException(
            IEnumerable<DependencyNotConstructableException> exceptions)
            : base(CreateMessage(exceptions))
        {
            Exceptions = exceptions;
        }

        private static string CreateMessage(
            IEnumerable<DependencyNotConstructableException> exceptions)
        {
            var mesasges = exceptions.Select(e => e.Message);
            return string.Join(Environment.NewLine, mesasges);
        }
    }
}
