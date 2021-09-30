namespace DI_Lite.Dependencies
{
    public interface IScopedDependency : IDependency
    {
        IDependency ToSingleton();
    }
}
