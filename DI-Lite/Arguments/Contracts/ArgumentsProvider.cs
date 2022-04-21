using DI_Lite.Arguments.Models;

namespace DI_Lite.Arguments.Contracts
{
    public abstract class ArgumentsProvider : IArgumentsProvider
    {
        public object Tag { get; }

        protected ArgumentsProvider(object tag = null)
        {
            Tag = tag;
        }

        public abstract object Get(ArgumentInfo info);
        public abstract bool Contains(ArgumentInfo info);
    }
}
