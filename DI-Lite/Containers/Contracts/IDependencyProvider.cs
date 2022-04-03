using System;

namespace DI_Lite
{
    public interface IDependencyProvider
    {
        T Get<T>(object tag = null);
        object Get(Type referenceType, object tag = null);

        bool Contains<T>(object tag = null);
        bool Contains(Type referenceType, object tag = null);
    }
}
