using DI_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerRemoveDependencyTest
    {
        private Container Container { get; set; }

        [TestInitialize]
        public void Before()
        {
            Container = new Container();

            Container.Factory(() => "string");
            Container.Factory("tag", () => "string");
            Container.Single(() => 0);
            Container.Single("tag", () => 0);
        }

        [TestMethod]
        public void RemovesAlldependenciesWithGivenType()
        {
            Container.Remove<string>();

            Assert.AreEqual(2, Container.Dependencies.Count());
            Assert.AreEqual(true, !Container.Dependencies.Any(d => d.Key.Type == typeof(string)));
        }

        [TestMethod]
        public void RemovesAlldependenciesWithGivenTag()
        {
            Container.Remove("tag");

            var dependency1 = Container.Dependencies.ElementAt(0);
            var dependency2 = Container.Dependencies.ElementAt(1);
            Assert.AreEqual(2, Container.Dependencies.Count());
            Assert.AreEqual(typeof(string), dependency1.Key.Type);
            Assert.AreEqual(null, dependency1.Key.Tag);
            Assert.AreEqual(typeof(int), dependency2.Key.Type);
            Assert.AreEqual(null, dependency2.Key.Tag);
        }

        [TestMethod]
        public void RemovesAlldependenciesWithGivenypeAndTag()
        {
            Container.Remove<string>("tag");

            var dependency1 = Container.Dependencies.ElementAt(0);
            var dependency2 = Container.Dependencies.ElementAt(1);
            var dependency3 = Container.Dependencies.ElementAt(2);
            Assert.AreEqual(3, Container.Dependencies.Count());
            Assert.AreEqual(typeof(string), dependency1.Key.Type);
            Assert.AreEqual(null, dependency1.Key.Tag);
            Assert.AreEqual(typeof(int), dependency2.Key.Type);
            Assert.AreEqual(null, dependency2.Key.Tag);
            Assert.AreEqual(typeof(int), dependency3.Key.Type);
            Assert.AreEqual("tag", dependency3.Key.Tag);
        }

        [TestMethod]
        public void RemovesAlldependenciesThatMatchesThePredicate()
        {
            Container.Remove(d =>
                d.Type != typeof(string) ||
                d.Tag != null);

            var dependency = Container.Dependencies.ElementAt(0);
            Assert.AreEqual(1, Container.Dependencies.Count());
            Assert.AreEqual(typeof(string), dependency.Key.Type);
            Assert.AreEqual(null, dependency.Key.Tag);
        }

        [TestMethod]
        public void RemovesDependencyWithGivenKey()
        {
            var key = Container.Dependencies.ElementAt(1).Key;

            Container.Remove(key);

            var dependency1 = Container.Dependencies.ElementAt(0);
            var dependency2 = Container.Dependencies.ElementAt(1);
            var dependency3 = Container.Dependencies.ElementAt(2);
            Assert.AreEqual(3, Container.Dependencies.Count());
            Assert.AreEqual(typeof(string), dependency1.Key.Type);
            Assert.AreEqual(null, dependency1.Key.Tag);
            Assert.AreEqual(typeof(int), dependency2.Key.Type);
            Assert.AreEqual(null, dependency2.Key.Tag);
            Assert.AreEqual(typeof(int), dependency3.Key.Type);
            Assert.AreEqual("tag", dependency3.Key.Tag);
        }

        [TestMethod]
        public void KeyDoesNotExist_DoesNothig()
        {
            var key = new DependencyKey(typeof(double), null);

            Container.Remove(key);

            Assert.AreEqual(4, Container.Dependencies.Count());
        }

        [TestMethod]
        public void RemovesAllDependenciesWithGivenKeys()
        {
            var keys = Container.Dependencies
                .Where(d => (string)d.Key.Tag == "tag")
                .Select(d => d.Key);

            Container.Remove(keys);

            var dependency1 = Container.Dependencies.ElementAt(0);
            var dependency2 = Container.Dependencies.ElementAt(1);
            Assert.AreEqual(2, Container.Dependencies.Count());
            Assert.AreEqual(typeof(string), dependency1.Key.Type);
            Assert.AreEqual(null, dependency1.Key.Tag);
            Assert.AreEqual(typeof(int), dependency2.Key.Type);
            Assert.AreEqual(null, dependency2.Key.Tag);
        }

        [TestMethod]
        public void KeysDoNotExist_DoesNothig()
        {
            var keys = new DependencyKey[] {
                new DependencyKey(typeof(double), null),
                new DependencyKey(typeof(bool), null),
            };

            Container.Remove(keys);

            Assert.AreEqual(4, Container.Dependencies.Count());
        }
    }
}
