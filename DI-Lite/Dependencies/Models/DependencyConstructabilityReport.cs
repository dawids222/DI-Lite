using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite.Dependencies.Models
{
    public class DependencyConstructabilityReport
    {
        public Type ReferenceType { get; }
        public Type ConcreteType { get; }
        public IEnumerable<Type> MissingDependencies { get; }

        public bool IsConstructable => !MissingDependencies.Any();
        public string Error => GetError();

        public DependencyConstructabilityReport(
            Type referenceType,
            Type concreteType,
            IEnumerable<Type> missingDependencies)
        {
            ReferenceType = referenceType;
            ConcreteType = concreteType;
            MissingDependencies = missingDependencies;
        }

        private string GetError()
        {
            if (!MissingDependencies.Any()) { return null; }
            var missingTypeNames = MissingDependencies.Select(t => $"'{t.FullName}'");
            var missingTypesString = string.Join(", ", missingTypeNames);
            return $"Instance of class '{ConcreteType.FullName}' can not be constructed because the following dependencies are not registered: {missingTypesString}.";
        }
    }
}
