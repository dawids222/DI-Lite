using LibLite.DI.Lite.Enums;

namespace LibLite.DI.Lite.Dependencies.Contracts
{
    public interface IDependency
    {
        DependencyType DependencyType { get; }
        object Get(IDependencyProvider provider);
    }
}
