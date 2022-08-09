using LibLite.DI.Lite.Arguments.Contracts;
using LibLite.DI.Lite.Arguments.Models;
using LibLite.DI.Lite.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibLite.DI.Lite.Arguments
{
    public class DelegateInvoker : IDelegateInvoker
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
                .Select(p => _provider.Get(new ArgumentInfo(p)))
                .ToArray();
        }
    }
}
