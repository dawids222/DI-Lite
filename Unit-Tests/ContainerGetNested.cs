using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerGetNested : ContainerBaseTest
    {
        [TestMethod]
        public void HandlesNestedDependenciesRegisteredInLogicalOrder()
        {
            Container.Single<IMockDependency>(() => new MockDepenedency());
            Container.Factory<IMockDependency>("", () => new MockDepenedency(Container.Get<IMockDependency>()));

            var dependency = Container.Get<IMockDependency>("");

            Assert.AreNotEqual(null, dependency.Inner);
            Assert.AreEqual(null, dependency.Inner.Inner);
        }

        [TestMethod]
        public void HandlesNestedDependenciesRegisteredInNotLogicalOrder()
        {
            Container.Single<IMockDependency>("", () => new MockDepenedency(Container.Get<IMockDependency>()));
            Container.Factory<IMockDependency>(() => new MockDepenedency());

            var dependency = Container.Get<IMockDependency>("");

            Assert.AreNotEqual(null, dependency.Inner);
            Assert.AreEqual(null, dependency.Inner.Inner);
        }
    }
}
