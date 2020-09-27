using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerSingleTest : ContainerBaseTest
    {
        [TestMethod]
        public void SingleAddsDependency()
        {
            Container.Single<string>(() => "");

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void SingleAddsDependencyWithDefaultKey()
        {
            Container.Single<string>(() => "");

            var key = new DependencyKey(typeof(string), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void SingleAddsDependencyWithSpecifiedKey()
        {
            Container.Single<string>("key", () => "");

            var key = new DependencyKey(typeof(string), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void SingleAddsMultipleDifferentDependenciesWithSameTag()
        {
            Container.Single<string>(() => "1");
            Container.Single<int>(() => 1);
            Container.Single<double>(() => 1.0);
            Container.Single<char>(() => '1');
            Container.Single<bool>(() => true);

            Assert.AreEqual(5, Container.Dependencies.Count());
        }

        [TestMethod]
        public void SingleAddsMultipleDifferentDependenciesWithSameType()
        {
            Container.Single<string>("1", () => "");
            Container.Single<string>("2", () => "");
            Container.Single<string>(new object(), () => "");
            Container.Single<string>(this, () => "");
            Container.Single<string>(12, () => "");

            Assert.AreEqual(5, Container.Dependencies.Count());
        }

        [TestMethod]
        public void SingleOverridesMultipleSameDependencies()
        {
            Container.Single<string>(() => "");
            Container.Single<string>(() => "");
            Container.Single<string>(() => "");
            Container.Single<string>(() => "");
            Container.Single<string>(() => "");

            Assert.AreEqual(1, Container.Dependencies.Count());
        }
    }
}
