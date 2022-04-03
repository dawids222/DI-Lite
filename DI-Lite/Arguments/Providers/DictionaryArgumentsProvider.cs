using DI_Lite.Arguments.Contracts;
using DI_Lite.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DI_Lite.Arguments.Providers
{
    public class DictionaryArgumentsProvider : ArgumentsProvider
    {
        private readonly IDictionary<string, string> _dictionary;
        private readonly static Type _sourceType = typeof(string);

        public DictionaryArgumentsProvider(IDictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }

        public override object Get(Type type, string name)
            => Convert(type, _dictionary[name]);

        private static object Convert(Type type, string value)
        {
            var converter = TypeDescriptor.GetConverter(type);
            return converter.ConvertFromInvariantString(value);
        }

        public override bool Contains(Type type, string name)
            => _dictionary.ContainsKey(name) && CanConvert(type, _dictionary[name]);

        private static (bool Success, object Result) TryConvert(Type targetType, string value)
        {
            var converter = TypeDescriptor.GetConverter(targetType);
            return converter.TryConvertFromInvariantString(value);
        }

        private static bool CanConvert(Type targetType, string value)
            => TryConvert(targetType, value).Success;
    }
}
