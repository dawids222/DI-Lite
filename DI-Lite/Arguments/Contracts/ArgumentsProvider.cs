using System;

namespace DI_Lite.Arguments.Contracts
{
    public abstract class ArgumentsProvider : IArgumentsProvider
    {
        public T Get<T>(string name) => (T)Get(typeof(T), name);
        public abstract object Get(Type type, string name);

        public bool Contains<T>(string name) => Contains(typeof(T), name);
        public abstract bool Contains(Type type, string name);
    }
}
