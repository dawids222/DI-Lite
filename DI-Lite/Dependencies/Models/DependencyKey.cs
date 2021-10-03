using System;

namespace DI_Lite
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

        public override bool Equals(object obj)
        {
            if (obj is DependencyKey o)
            {
                return (Type == o.Type && Tag == o.Tag);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Type.GetHashCode();
            hash = hash * 23 + Tag?.GetHashCode() ?? 0;
            return hash;
        }
    }
}
