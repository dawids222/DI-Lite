using LibLite.DI.Lite.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LibLite.DI.Lite
{
    public class DependencyKey
    {
        public Type Type { get; }
        public object Tag { get; }

        public DependencyKey(Type type, object tag)
        {
            Type = type;
            Tag = tag;
        }

        public DependencyKey(ParameterInfo info)
        {
            Type = info.ParameterType;
            Tag = info.GetCustomAttribute<WithTagAttribute>()?.Tag;
        }

        public override bool Equals(object obj)
        {
            return obj is DependencyKey key &&
                   EqualityComparer<Type>.Default.Equals(Type, key.Type) &&
                   EqualityComparer<object>.Default.Equals(Tag, key.Tag);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Tag);
        }
    }
}
