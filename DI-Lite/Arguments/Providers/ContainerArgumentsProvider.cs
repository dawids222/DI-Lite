using DI_Lite.Arguments.Contracts;
using System;

namespace DI_Lite.Arguments.Providers
{
    public class ContainerArgumentsProvider : ArgumentsProvider
    {
        private readonly IDependencyProvider _container;

        public ContainerArgumentsProvider(IDependencyProvider dependencyProvider)
        {
            _container = dependencyProvider;
        }

        public override object Get(Type type, string name) => _container.Get(type);

        public override bool Contains(Type type, string name) => _container.Contains(type);
    }
}
