using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite.Dependencies.Models
{
    public class ConstructabilityReport
    {
        public Type ReferenceType { get; set; }
        public Type ConcreteType { get; set; }
        public IEnumerable<Type> MissingDependencies { get; set; }

        public bool IsConstructable => !MissingDependencies.Any();

        public ConstructabilityReport(
            Type referenceType,
            Type concreteType,
            IEnumerable<Type> missingDependencies)
        {
            ReferenceType = referenceType;
            ConcreteType = concreteType;
            MissingDependencies = missingDependencies;
        }
    }
}
