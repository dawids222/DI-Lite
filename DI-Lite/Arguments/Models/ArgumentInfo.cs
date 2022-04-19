using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI_Lite.Arguments.Models
{
    public class ArgumentInfo
    {
        public Type Type { get; init; }
        public string Name { get; init; }
        public Attribute[] Attributes { get; init; } = Array.Empty<Attribute>();

        public ArgumentInfo() { }
        public ArgumentInfo(ParameterInfo parameter)
        {
            Type = parameter.ParameterType;
            Name = parameter.Name;
            Attributes = parameter.GetCustomAttributes().ToArray();
        }

        public override bool Equals(object obj)
        {
            return obj is ArgumentInfo info &&
                   EqualityComparer<Type>.Default.Equals(Type, info.Type) &&
                   Name == info.Name &&
                   EqualityComparer<Attribute[]>.Default.Equals(Attributes, info.Attributes);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Name, Attributes);
        }
    }
}
