namespace LibLite.DI.Lite.Dependencies.Contracts
{
    public interface IScopedDependency : IDependency
    {
        IDependency ToScopedSingleton();
    }
}
