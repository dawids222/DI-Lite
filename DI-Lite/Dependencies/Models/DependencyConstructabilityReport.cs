using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite.Dependencies.Models
{
    public class DependencyConstructabilityReport
    {
        public Type ReferenceType { get; }
        public Type ConcreteType { get; }
        public IEnumerable<DependencyKey> MissingDependencies { get; }

        public bool IsConstructable => !MissingDependencies.Any();
        public string Error => GetError();

        public DependencyConstructabilityReport(
            Type referenceType,
            Type concreteType,
            IEnumerable<DependencyKey> missingDependencies)
        {
            ReferenceType = referenceType;
            ConcreteType = concreteType;
            MissingDependencies = missingDependencies;
        }

        private string GetError()
        {
            if (!MissingDependencies.Any()) { return null; }
            var missingDependencies = MissingDependencies.Select(x => $"{{Tag: '{x.Tag}', Type: '{x.Type.FullName}'}}");
            var missingDependenciesString = string.Join(", ", missingDependencies);
            return $"Instance of class '{ConcreteType.FullName}' can not be constructed because the following dependencies are not registered: {missingDependenciesString}.";
        }
    }
}
