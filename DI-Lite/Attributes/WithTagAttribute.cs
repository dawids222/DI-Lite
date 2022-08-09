using System;

namespace LibLite.DI.Lite.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class WithTagAttribute : Attribute
    {
        public object Tag { get; }

        public WithTagAttribute(object tag)
        {
            Tag = tag;
        }
    }
}
