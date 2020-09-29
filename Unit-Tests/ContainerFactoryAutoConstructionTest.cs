using DI_Lite;
using DI_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerFactoryAutoConstructionTest : ContainerBaseTest
    {
        [TestMethod]
        public void AddsDependency()
        {
            Container.Factory<IMockDependency, MockDepenedency>();

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsDependencyWithShorthand()
        {
            Container.Factory<MockDepenedency>();

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsDependencyWithDefaultKey()
        {
            Container.Factory<IMockDependency>(() => new MockDepenedency());
            Container.Factory<MockDepenedency, MockDepenedency>();

            var key = new DependencyKey(typeof(MockDepenedency), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(1).Key);
        }

        [TestMethod]
        public void AddsDependencyWithSpecifiedKey()
        {
            Container.Factory<IMockDependency>(() => new MockDepenedency());
            Container.Factory<IMockDependency, MockDepenedency>("key");

            var key = new DependencyKey(typeof(IMockDependency), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(1).Key);
        }

        [TestMethod]
        public void ReturnsValueUsingLongestConstructor()
        {
            Container.Factory<IMockDependency>(() => new MockDepenedency());
            Container.Factory<MockDepenedency, MockDepenedency>();

            var dep = Container.Get<MockDepenedency>();

            Assert.AreNotEqual(null, dep.Inner);
        }

        [TestMethod]
        public void ReturnsDifferentValue()
        {
            Container.Factory<IMockDependency>(() => new MockDepenedency());
            Container.Factory<MockDepenedency, MockDepenedency>();

            var dep1 = Container.Get<MockDepenedency>();
            var dep2 = Container.Get<MockDepenedency>();

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void ReturnsDifferentValueWithShortHand()
        {
            Container.Factory<IMockDependency>(() => new MockDepenedency());
            Container.Factory<MockDepenedency>();

            var dep1 = Container.Get<MockDepenedency>();
            var dep2 = Container.Get<MockDepenedency>();

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredException()
        {
            Container.Factory<MockDepenedency, MockDepenedency>();

            var dep = Container.Get<MockDepenedency>();

            Assert.AreNotEqual(null, dep.Inner);
        }
    }
}
