using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests
{
    [TestClass]
    public abstract class ContainerAddDependencyAbstractionBaseTest : ContainerBaseTest
    {
        protected abstract void AddDependency<T>(object tag, Func<T> creator);
        protected void AddDependency<T>(Func<T> creator)
        {
            AddDependency<T>(null, creator);
        }

        protected abstract void AddAutoConstructingDependency<T, R>(object tag) where R : class, T;
        protected void AddAutoConstructingDependency<T, R>() where R : class, T
        {
            AddAutoConstructingDependency<T, R>(null);
        }
        protected void AddAutoConstructingDependency<T>(object tag) where T : class
        {
            AddAutoConstructingDependency<T, T>(tag);
        }
        protected void AddAutoConstructingDependency<T>() where T : class
        {
            AddAutoConstructingDependency<T, T>(null);
        }
    }
}
