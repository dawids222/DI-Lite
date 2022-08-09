using System;

namespace LibLite.DI.Lite.Arguments.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromProviderAttribute : Attribute
    {
        public object Tag { get; }

        public FromProviderAttribute(object tag)
        {
            Tag = tag;
        }
    }
}
