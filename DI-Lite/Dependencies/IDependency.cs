namespace DI_Lite.Dependencies
{
    public interface IDependency
    {
        object Get(IDependencyProvider provider);
    }
}
