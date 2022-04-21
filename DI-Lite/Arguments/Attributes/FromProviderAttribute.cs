using System;

namespace DI_Lite.Arguments.Attributes
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
