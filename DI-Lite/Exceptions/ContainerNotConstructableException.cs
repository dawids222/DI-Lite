using LibLite.DI.Lite.Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibLite.DI.Lite.Exceptions
{
    public class ContainerNotConstructableException : Exception
    {
        public IEnumerable<DependencyConstructabilityReport> FailedReports { get; }

        public ContainerNotConstructableException(
            IEnumerable<DependencyConstructabilityReport> failedReports)
            : base(CreateMessage(failedReports))
        {
            FailedReports = failedReports;
        }

        private static string CreateMessage(
            IEnumerable<DependencyConstructabilityReport> failedReports)
        {
            var errors = failedReports.Select(r => r.Error);
            return string.Join(Environment.NewLine, errors);
        }
    }
}
