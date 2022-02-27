using DI_Lite.Dependencies.Models;

namespace DI_Lite.Dependencies.Contracts
{
    internal interface IAutoConstructedDependency : IDependency
    {
        DependencyConstructabilityReport GetConstructabilityReport(Container container);
        bool IsConstructable(Container container);
    }
}
