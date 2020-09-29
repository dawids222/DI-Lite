using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerGetFactoryTest : ContainerGetDependencyBaseTest
    {
        protected override bool SameKeyProducesSameDependency => false;

        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.Factory(tag, creator);
        }
    }
}
