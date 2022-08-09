using System;
using System.ComponentModel;

namespace LibLite.DI.Lite.Extensions
{
    internal static class TypeConverterExtensions
    {
        internal static (bool Success, object Result) TryConvertFromInvariantString(this TypeConverter converter, string value)
        {
            try
            {
                var result = converter.ConvertFromInvariantString(value);
                return (true, result);
            }
            catch (Exception)
            {
                return (false, null);
            }
        }
    }
}
