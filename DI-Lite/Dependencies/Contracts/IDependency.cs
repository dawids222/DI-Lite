using DI_Lite.Enums;

namespace DI_Lite.Dependencies.Contracts
{
    public interface IDependency
    {
        DependencyType DependencyType { get; }
        object Get(IDependencyProvider provider);
    }
}
