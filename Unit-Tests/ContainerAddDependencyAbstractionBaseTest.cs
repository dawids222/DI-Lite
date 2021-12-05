using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests
{
    [TestClass]
    public abstract class ContainerAddDependencyAbstractionBaseTest : ContainerBaseTest
    {
        protected abstract void AddDependency<T>(object tag, Func<T> creator);
        protected abstract void AddDependency<T>(Func<T> creator);
        protected abstract void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator);
        protected abstract void AddDependency<T>(Func<IDependencyProvider, T> creator);

        protected abstract void AddAutoConstructingDependency<T, R>(object tag) where R : class, T;
        protected abstract void AddAutoConstructingDependency<T, R>() where R : class, T;
        protected abstract void AddAutoConstructingDependency<T>(object tag) where T : class;
        protected abstract void AddAutoConstructingDependency<T>() where T : class;
    }
}
