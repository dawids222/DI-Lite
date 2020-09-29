using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerFactoryTest : ContainerAddDependencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.Factory(tag, creator);
        }
    }
}
