using System;

namespace DI_Lite.Exceptions
{
    public class DependencyRetrievalRequiresScopeException : Exception
    {
        public DependencyRetrievalRequiresScopeException()
            : base($"This dependency can only be retrieved from scope. {nameof(ScopedContainer)} can be created via {nameof(Container)}") { }
    }
}
