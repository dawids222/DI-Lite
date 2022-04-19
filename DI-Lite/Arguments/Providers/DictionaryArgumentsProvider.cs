﻿using DI_Lite.Arguments.Contracts;
using DI_Lite.Arguments.Models;
using DI_Lite.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DI_Lite.Arguments.Providers
{
    public class DictionaryArgumentsProvider : ArgumentsProvider
    {
        private readonly IDictionary<string, string> _dictionary;

        public DictionaryArgumentsProvider(IDictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }

        public override object Get(ArgumentInfo info)
            => Convert(info.Type, _dictionary[info.Name]);

        private static object Convert(Type type, string value)
        {
            var converter = TypeDescriptor.GetConverter(type);
            return converter.ConvertFromInvariantString(value);
        }

        public override bool Contains(ArgumentInfo info) =>
            _dictionary.ContainsKey(info.Name) &&
            CanConvert(info.Type, _dictionary[info.Name]);

        private static (bool Success, object Result) TryConvert(Type targetType, string value)
        {
            var converter = TypeDescriptor.GetConverter(targetType);
            return converter.TryConvertFromInvariantString(value);
        }

        private static bool CanConvert(Type targetType, string value)
            => TryConvert(targetType, value).Success;
    }
}
