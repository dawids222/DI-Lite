using LibLite.DI.Lite.Dependencies.Models;

namespace LibLite.DI.Lite.Dependencies.Contracts
{
    internal interface IAutoConstructedDependency : IDependency
    {
        DependencyConstructabilityReport GetConstructabilityReport(Container container);
        bool IsConstructable(Container container);
    }
}
