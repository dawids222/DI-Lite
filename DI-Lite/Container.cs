using System;
using System.Collections.Generic;

namespace DI_Lite
{
    public class Container
    {
        private Dictionary<DependencyKey, object> dependencies { get; } = new Dictionary<DependencyKey, object>();
        public IEnumerable<KeyValuePair<DependencyKey, object>> Dependencies { get => dependencies; }

        public void Single<T>(Func<T> creator)
        {
            Single(null, creator);
        }

        public void Single<T>(object tag, Func<T> creator)
        {
            var key = new DependencyKey(typeof(T), tag);
            if (dependencies.ContainsKey(key))
            {
                dependencies.Remove(key);
            }
            dependencies.Add(key, creator);
        }
    }
}
