using DI_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unit_Tests.Models;

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

    [TestClass]
    public class ContainerGetFactoryTest : ContainerGetDependencyBaseTest
    {
        protected override bool SameKeyProducesSameDependency => false;

        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.Factory(tag, creator);
        }
    }

    [TestClass]
    public abstract class ContainerGetDependencyBaseTest : ContainerBaseTest
    {
        private Func<IMockDependency> CreateFunction { get; set; }
        protected abstract bool SameKeyProducesSameDependency { get; }

        protected abstract void AddDependency<T>(object tag, Func<T> creator);

        private void AddDependency<T>(Func<T> creator)
        {
            AddDependency(null, creator);
        }

        [TestInitialize]
        public override void Before()
        {
            base.Before();
            CreateFunction = () => new MockDepenedency();
        }

        [TestMethod]
        public void GetSameTypeWithoutTags()
        {
            AddDependency(CreateFunction);

            var dep1 = Container.Get<IMockDependency>();
            var dep2 = Container.Get<IMockDependency>();

            if (SameKeyProducesSameDependency)
                Assert.AreEqual(dep1, dep2);
            else
                Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithSameTags()
        {
            AddDependency("tag", CreateFunction);

            var dep1 = Container.Get<IMockDependency>("tag");
            var dep2 = Container.Get<IMockDependency>("tag");

            if (SameKeyProducesSameDependency)
                Assert.AreEqual(dep1, dep2);
            else
                Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithDifferentTags()
        {
            AddDependency("tag", CreateFunction);
            AddDependency("gat", CreateFunction);

            var dep1 = Container.Get<IMockDependency>("tag");
            var dep2 = Container.Get<IMockDependency>("gat");

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void GetDifferentTypesWithoutTags()
        {
            AddDependency(CreateFunction);
            AddDependency(() => "");

            var dep1 = Container.Get<IMockDependency>();
            var dep2 = Container.Get<string>();

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredException()
        {
            Container.Get<IMockDependency>();
        }
    }
}
