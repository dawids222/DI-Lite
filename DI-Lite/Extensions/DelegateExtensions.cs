using System;
using System.Threading.Tasks;

namespace DI_Lite.Extensions
{
    internal static class DelegateExtensions
    {
        internal static async Task<object> DynamicInvokeAsync(this Delegate del, params object[] args)
        {
            object result;
            if (del.ReturnsTask())
            {
                result = await (dynamic)del.DynamicInvoke(args);
                return del.ReturnsGenericType() ? result : new object();
            }

            result = del.DynamicInvoke(args);
            return !del.ReturnsVoid() ? result : new object();
        }

        internal static bool ReturnsTask(this Delegate del)
            => del.Method.ReturnType.IsAssignableTo(typeof(Task));

        internal static bool ReturnsGenericType(this Delegate del)
            => del.Method.ReturnType.IsConstructedGenericType;

        internal static bool ReturnsVoid(this Delegate del)
            => del.Method.ReturnType == typeof(void);
    }
}
