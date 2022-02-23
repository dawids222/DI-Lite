namespace DI_Lite.Dependencies.Contracts
{
    internal interface IAutoConstructedDependency : IDependency
    {
        void ThrowIfIsNotConstructable(Container container);
    }
}
