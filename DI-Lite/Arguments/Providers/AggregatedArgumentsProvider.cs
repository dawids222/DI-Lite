using DI_Lite.Arguments.Contracts;
using System;
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

        public override object Get(Type type, string name) => _providers
            .First(p => p.Contains(type, name))
            .Get(type, name);

        public override bool Contains(Type type, string name) => _providers
            .Any(p => p.Contains(type, name));
    }
}
