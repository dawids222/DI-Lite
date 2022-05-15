namespace DI_Lite.Dependencies.Contracts
{
    public interface IScopedDependency : IDependency
    {
        IDependency ToScopedSingleton();
    }
}
