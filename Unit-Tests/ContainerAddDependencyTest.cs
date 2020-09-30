using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerSingleTest : ContainerAddDependencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.Single(tag, creator);
        }

        protected override void AddAutoConstructingDependency<T, R>(object tag)
        {
            Container.Single<T, R>(tag);
        }
    }

    [TestClass]
    public class ContainerFactoryTest : ContainerAddDependencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.Factory(tag, creator);
        }

        protected override void AddAutoConstructingDependency<T, R>(object tag)
        {
            Container.Factory<T, R>(tag);
        }
    }

    [TestClass]
    public abstract class ContainerAddDependencyBaseTest : ContainerAddDependencyAbstractionBaseTest
    {
        [TestMethod]
        public void AddsDependency()
        {
            AddDependency(() => "");

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsDependencyWithDefaultKey()
        {
            AddDependency(() => "");

            var key = new DependencyKey(typeof(string), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void AddsDependencyWithSpecifiedKey()
        {
            AddDependency("key", () => "");

            var key = new DependencyKey(typeof(string), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void AddsMultipleDifferentDependenciesWithSameTag()
        {
            AddDependency(() => "1");
            AddDependency(() => 1);
            AddDependency(() => 1.0);
            AddDependency(() => '1');
            AddDependency(() => true);

            Assert.AreEqual(5, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsMultipleDifferentDependenciesWithSameType()
        {
            AddDependency("1", () => "");
            AddDependency("2", () => "");
            AddDependency(new object(), () => "");
            AddDependency(this, () => "");
            AddDependency(12, () => "");

            Assert.AreEqual(5, Container.Dependencies.Count());
        }

        [TestMethod]
        public void OverridesMultipleSameDependencies()
        {
            AddDependency(() => "");
            AddDependency(() => "");
            AddDependency(() => "");
            AddDependency(() => "");
            AddDependency(() => "");

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsAutoConstructingDependency()
        {
            AddAutoConstructingDependency<IMockDependency, MockDepenedency>();

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsAutoConstructingDependencyWithShorthand()
        {
            AddAutoConstructingDependency<MockDepenedency>();

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsAutoConstructingDependencyWithDefaultKey()
        {
            AddDependency<IMockDependency>(() => new MockDepenedency());
            AddAutoConstructingDependency<MockDepenedency, MockDepenedency>();

            var key = new DependencyKey(typeof(MockDepenedency), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(1).Key);
        }

        [TestMethod]
        public void AddsAutoConstructingDependencyWithSpecifiedKey()
        {
            AddDependency<IMockDependency>(() => new MockDepenedency());
            AddAutoConstructingDependency<IMockDependency, MockDepenedency>("key");

            var key = new DependencyKey(typeof(IMockDependency), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(1).Key);
        }
    }
}
