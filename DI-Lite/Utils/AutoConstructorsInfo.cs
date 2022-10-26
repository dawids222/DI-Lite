using LibLite.DI.Lite.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LibLite.DI.Lite.Utils
{
    internal class AutoConstructorsInfo
    {
        private readonly IEnumerable<ConstructorInfo> _constructors;
        private readonly IEnumerable<ConstructorInfo> _decoratedConstructors;

        public int Total => _constructors.Count();
        public int Decorated => _decoratedConstructors.Count();

        public AutoConstructorsInfo(Type type)
        {
            var constructors = type.GetConstructors();
            _constructors = constructors;
            _decoratedConstructors = constructors
                .Where(x => x.GetCustomAttribute<UseConstructorAttribute>() is not null)
                .ToList();
        }

        public ConstructorInfo First() => _constructors.First();
        public ConstructorInfo FirstDecorated() => _decoratedConstructors.First();
    }
}
