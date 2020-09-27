using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerFactoryTest : ContainerBaseTest
    {
        [TestMethod]
        public void FactoryAddsDependency()
        {
            Container.Factory<string>(() => "");

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void FactoryAddsDependencyWithDefaultKey()
        {
            Container.Factory<string>(() => "");

            var key = new DependencyKey(typeof(string), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void FactoryAddsDependencyWithSpecifiedKey()
        {
            Container.Factory<string>("key", () => "");

            var key = new DependencyKey(typeof(string), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void FactoryAddsMultipleDifferentDependenciesWithSameTag()
        {
            Container.Factory<string>(() => "1");
            Container.Factory<int>(() => 1);
            Container.Factory<double>(() => 1.0);
            Container.Factory<char>(() => '1');
            Container.Factory<bool>(() => true);

            Assert.AreEqual(5, Container.Dependencies.Count());
        }

        [TestMethod]
        public void FactoryAddsMultipleDifferentDependenciesWithSameType()
        {
            Container.Factory<string>("1", () => "");
            Container.Factory<string>("2", () => "");
            Container.Factory<string>(new object(), () => "");
            Container.Factory<string>(this, () => "");
            Container.Factory<string>(12, () => "");

            Assert.AreEqual(5, Container.Dependencies.Count());
        }

        [TestMethod]
        public void FactoryOverridesMultipleSameDependencies()
        {
            Container.Factory<string>(() => "");
            Container.Factory<string>(() => "");
            Container.Factory<string>(() => "");
            Container.Factory<string>(() => "");
            Container.Factory<string>(() => "");

            Assert.AreEqual(1, Container.Dependencies.Count());
        }
    }
}
