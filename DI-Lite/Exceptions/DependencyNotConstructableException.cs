using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite.Exceptions
{
    public class DependencyNotConstructableException : Exception
    {
        public Type ConcreteType { get; }
        public IEnumerable<Type> MissingTypes { get; }

        public DependencyNotConstructableException(
            Type concreteType,
            IEnumerable<Type> missingTypes)
            : base(CreateMessage(concreteType, missingTypes))
        {
            ConcreteType = concreteType;
            MissingTypes = missingTypes;
        }

        private static string CreateMessage(
            Type concreteType,
            IEnumerable<Type> missingTypes)
        {
            var missingTypeNames = missingTypes.Select(t => $"'{t.FullName}'");
            var missingTypesString = string.Join(", ", missingTypeNames);
            return $"Instance of class '{concreteType.FullName}' can not be constructed because the following dependencies are not registered: {missingTypesString}.";
        }

        public override bool Equals(object obj)
        {
            return obj is DependencyNotConstructableException exception &&
                   Message == exception.Message;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Message);
        }
    }
}
