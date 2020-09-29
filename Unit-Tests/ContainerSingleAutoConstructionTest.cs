using DI_Lite;
using DI_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerSingleAutoConstructionTest : ContainerBaseTest
    {
        [TestMethod]
        public void AddsDependency()
        {
            Container.Single<IMockDependency, MockDepenedency>();

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsDependencyWithDefaultKey()
        {
            Container.Single<IMockDependency>(() => new MockDepenedency());
            Container.Single<MockDepenedency, MockDepenedency>();

            var key = new DependencyKey(typeof(MockDepenedency), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(1).Key);
        }

        [TestMethod]
        public void AddsDependencyWithSpecifiedKey()
        {
            Container.Single<IMockDependency>(() => new MockDepenedency());
            Container.Single<IMockDependency, MockDepenedency>("key");

            var key = new DependencyKey(typeof(IMockDependency), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(1).Key);
        }

        [TestMethod]
        public void ReturnsValueUsingLongestConstructor()
        {
            Container.Single<IMockDependency>(() => new MockDepenedency());
            Container.Single<MockDepenedency, MockDepenedency>();

            var dep = Container.Get<MockDepenedency>();

            Assert.AreNotEqual(null, dep.Inner);
        }

        [TestMethod]
        public void ReturnsSameValue()
        {
            Container.Single<IMockDependency>(() => new MockDepenedency());
            Container.Single<MockDepenedency, MockDepenedency>();

            var dep1 = Container.Get<MockDepenedency>();
            var dep2 = Container.Get<MockDepenedency>();

            Assert.AreEqual(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredException()
        {
            Container.Single<MockDepenedency, MockDepenedency>();

            var dep = Container.Get<MockDepenedency>();

            Assert.AreNotEqual(null, dep.Inner);
        }
    }
}
