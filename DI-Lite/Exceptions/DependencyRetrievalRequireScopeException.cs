using System;

namespace DI_Lite.Exceptions
{
    public class DependencyRetrievalRequireScopeException : Exception
    {
        public DependencyRetrievalRequireScopeException()
            : base($"This dependency can only be retrieved from scope. {nameof(ScopedContainer)} can be created via {nameof(Container)}.") { }
    }
}
