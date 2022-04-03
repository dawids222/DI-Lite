using System;

namespace DI_Lite.Arguments.Contracts
{
    public interface IArgumentsProvider
    {
        T Get<T>(string name);
        object Get(Type type, string name);
        bool Contains<T>(string name);
        bool Contains(Type type, string name);
    }
}
