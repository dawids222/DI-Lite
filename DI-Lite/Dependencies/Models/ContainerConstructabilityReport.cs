using System.Collections.Generic;
using System.Linq;

namespace LibLite.DI.Lite.Dependencies.Models
{
    public class ContainerConstructabilityReport
    {
        public IEnumerable<DependencyConstructabilityReport> ConstructabilityReports { get; }

        public ContainerConstructabilityReport(IEnumerable<DependencyConstructabilityReport> constructabilityReports)
        {
            ConstructabilityReports = constructabilityReports;
        }

        public bool IsConstructable => ConstructabilityReports.All(r => r.IsConstructable);
        public IEnumerable<DependencyConstructabilityReport> FailedConstructabilityReports
            => ConstructabilityReports.Where(r => !r.IsConstructable);
    }
}
