using DI_Lite.Arguments.Contracts;
using DI_Lite.Arguments.Models;
using System.Collections.Generic;
using System.Linq;

namespace DI_Lite.Arguments.Providers
{
    public class AggregatedArgumentsProvider : ArgumentsProvider
    {
        private readonly IEnumerable<IArgumentsProvider> _providers;

        public AggregatedArgumentsProvider(IEnumerable<IArgumentsProvider> providers)
        {
            _providers = providers;
        }

        // TODO: If no match throwing or returning null should be configurable?
        public override object Get(ArgumentInfo info) => _providers
            .First(p => p.Contains(info))
            .Get(info);

        public override bool Contains(ArgumentInfo info) => _providers
            .Any(p => p.Contains(info));
    }
}
