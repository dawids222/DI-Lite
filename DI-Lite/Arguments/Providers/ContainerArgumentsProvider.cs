using DI_Lite.Arguments.Contracts;
using DI_Lite.Arguments.Models;
using DI_Lite.Attributes;
using System.Linq;

namespace DI_Lite.Arguments.Providers
{
    public class ContainerArgumentsProvider : ArgumentsProvider
    {
        private readonly IDependencyProvider _container;

        public ContainerArgumentsProvider(
            IDependencyProvider dependencyProvider,
            object tag = null) : base(tag)
        {
            _container = dependencyProvider;
        }

        public override object Get(ArgumentInfo info) => _container.Get(info.Type, GetTag(info));
        public override bool Contains(ArgumentInfo info) => _container.Contains(info.Type, GetTag(info));

        private static object GetTag(ArgumentInfo info)
        {
            return info
                .Attributes
                .OfType<WithTagAttribute>()
                .FirstOrDefault()
                ?.Tag;
        }
    }
}
