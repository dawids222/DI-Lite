using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerContainsGenericTest : DependencyProviderContainsTestBase
    {
        protected override bool Contains<T>(object tag)
            => Container.Contains<T>(tag);
    }

    [TestClass]
    public class ContainerContainsTypeTest : DependencyProviderContainsTestBase
    {
        protected override bool Contains<T>(object tag)
            => Container.Contains(typeof(T), tag);
    }

    [TestClass]
    public class ScopedContainerContainsGenericTest : DependencyProviderContainsTestBase
    {
        protected override bool Contains<T>(object tag)
            => Container.CreateScope().Contains<T>(tag);
    }

    [TestClass]
    public class ScopedContainerContainsTypeTest : DependencyProviderContainsTestBase
    {
        protected override bool Contains<T>(object tag)
            => Container.CreateScope().Contains(typeof(T), tag);
    }

    public abstract class DependencyProviderContainsTestBase : ContainerBaseTest
    {
        protected abstract bool Contains<T>(object tag);
        protected bool Contains<T>() => Contains<T>(null);

        [TestMethod]
        public void Contains_DependencyRegistered_ReturnsTrue()
        {
            Container.Single("");

            var result = Contains<string>();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Contains_DependencyNotRegistered_ReturnsFalse()
        {
            var result = Contains<string>();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Contains_DifferentDependencyRegistered_ReturnsFalse()
        {
            Container.Single(1);

            var result = Contains<string>();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Contains_WithTag_DependencyRegistered_ReturnsTrue()
        {
            Container.Single("tag", "");

            var result = Contains<string>("tag");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Contains_WithTag_DifferentDependencyRegistered_ReturnsFalse()
        {
            Container.Single("tag", 1);

            var result = Contains<string>("tag");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Contains_WithTag_DependencyHosNoTag_ReturnsFalse()
        {
            Container.Single("");

            var result = Contains<string>("tag");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Contains_WithTag_TagMissmatch_ReturnsFalse()
        {
            Container.Single("tag", "");

            var result = Contains<string>("gat");

            Assert.IsFalse(result);
        }
    }
}
