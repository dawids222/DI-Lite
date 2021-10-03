namespace DI_Lite
{
    public interface IDependencyProvider
    {
        T Get<T>(object tag = null);
    }
}
