using DI_Lite.Arguments.Models;

namespace DI_Lite.Arguments.Contracts
{
    public abstract class ArgumentsProvider : IArgumentsProvider
    {
        public abstract object Get(ArgumentInfo info);
        public abstract bool Contains(ArgumentInfo info);
    }
}
