using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerSingleTest : ContainerAddDependencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.Single(tag, creator);
        }
    }

    [TestClass]
    public class ContainerFactoryTest : ContainerAddDependencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.Factory(tag, creator);
        }
    }

    [TestClass]
    public abstract class ContainerAddDependencyBaseTest : ContainerBaseTest
    {
        protected abstract void AddDependency<T>(object tag, Func<T> creator);
        private void AddDependency<T>(Func<T> creator)
        {
            AddDependency<T>(null, creator);
        }

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
    }
}
