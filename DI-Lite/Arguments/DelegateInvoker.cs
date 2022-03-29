using DI_Lite.Arguments.Contracts;
using DI_Lite.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DI_Lite.Arguments
{
    public class DelegateInvoker
    {
        private readonly Delegate _delegate;
        private readonly IArgumentsProvider _provider;

        public DelegateInvoker(Delegate del, IArgumentsProvider provider)
        {
            _delegate = del;
            _provider = provider;
        }

        public async Task<object> InvokeAsync()
        {
            var args = GetArguments();
            return await _delegate.DynamicInvokeAsync(args);
        }

        public object Invoke()
        {
            var args = GetArguments();
            return _delegate.DynamicInvoke(args);
        }

        private object[] GetArguments()
        {
            return _delegate.Method
                .GetParameters()
                .Select(p => _provider.Get(p.ParameterType, p.Name))
                .ToArray();
        }
    }
}
