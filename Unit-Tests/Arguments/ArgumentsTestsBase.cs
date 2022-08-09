using LibLite.DI.Lite.Arguments.Models;
using System;

namespace LibLite.DI.Lite.Tests.Arguments
{
    public abstract class ArgumentsTestsBase
    {
        protected static ArgumentInfo MockInfo<Type>(string name) => MockInfo(typeof(Type), name);
        protected static ArgumentInfo MockInfo<Type>(string name, Attribute[] attributes) => MockInfo(typeof(Type), name, attributes);
        protected static ArgumentInfo MockInfo<Type>() => MockInfo(typeof(Type), "");
        protected static ArgumentInfo MockInfo(Type type, string name) => MockInfo(type, name, Array.Empty<Attribute>());

        protected static ArgumentInfo MockInfo(Type type, string name, Attribute[] attributes)
        {
            return new ArgumentInfo
            {
                Type = type,
                Name = name,
                Attributes = attributes,
            };
        }
    }
}
