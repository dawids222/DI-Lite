using System.Threading.Tasks;

namespace DI_Lite.Arguments.Contracts
{
    public interface IDelegateInvoker
    {
        Task<object> InvokeAsync();
        object Invoke();
    }
}
