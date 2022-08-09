using LibLite.DI.Lite.Arguments.Models;

namespace LibLite.DI.Lite.Arguments.Contracts
{
    public interface IArgumentsProvider
    {
        object Tag { get; }

        object Get(ArgumentInfo info);
        bool Contains(ArgumentInfo info);
    }
}
