using DI_Lite.Arguments.Attributes;
using DI_Lite.Arguments.Contracts;
using DI_Lite.Arguments.Models;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite.Arguments.Providers
{
    public class AggregatedArgumentsProvider : ArgumentsProvider
    {
        private readonly IEnumerable<IArgumentsProvider> _providers;

        public AggregatedArgumentsProvider(
            IEnumerable<IArgumentsProvider> providers,
            object tag = null) : base(tag)
        {
            _providers = providers;
        }

        // TODO: If no match throwing or returning null should be configurable?
        public override object Get(ArgumentInfo info)
            => GetProvidersForArgument(info)
               .First(p => p.Contains(info))
               .Get(info);

        public override bool Contains(ArgumentInfo info)
            => GetProvidersForArgument(info)
               .Any(p => p.Contains(info));

        private IEnumerable<IArgumentsProvider> GetProvidersForArgument(ArgumentInfo info)
        {
            var tag = info
                .Attributes
                .OfType<FromProviderAttribute>()
                .FirstOrDefault()
                ?.Tag;

            if (tag is null) { return _providers; }

            var comparer = EqualityComparer<object>.Default;
            return _providers
                    .Where(x => comparer.Equals(x.Tag, tag))
                    .ToArray();
        }
    }
}
