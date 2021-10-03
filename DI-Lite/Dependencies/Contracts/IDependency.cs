namespace DI_Lite.Dependencies.Contracts
{
    public interface IDependency
    {
        object Get(IDependencyProvider provider);
    }
}
