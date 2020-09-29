using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerGetSingleTest : ContainerGetDependencyBaseTest
    {
        protected override bool SameKeyProducesSameDependency => true;

        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.Single(tag, creator);
        }
    }
}
