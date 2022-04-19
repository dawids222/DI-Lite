using DI_Lite.Arguments.Models;

namespace DI_Lite.Arguments.Contracts
{
    public interface IArgumentsProvider
    {
        object Get(ArgumentInfo info);
        bool Contains(ArgumentInfo info);
    }
}
