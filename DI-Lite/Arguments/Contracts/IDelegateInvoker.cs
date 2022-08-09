using System.Threading.Tasks;

namespace LibLite.DI.Lite.Arguments.Contracts
{
    public interface IDelegateInvoker
    {
        Task<object> InvokeAsync();
        object Invoke();
    }
}
