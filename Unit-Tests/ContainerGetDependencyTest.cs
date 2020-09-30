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

        protected override void AddAutoConstructingDependency<T, R>(object tag)
        {
            Container.Single<T, R>(tag);
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

        protected override void AddAutoConstructingDependency<T, R>(object tag)
        {
            Container.Factory<T, R>(tag);
        }
    }

    [TestClass]
    public abstract class ContainerGetDependencyBaseTest : ContainerAddDependencyAbstractionBaseTest
    {
        private Func<IMockDependency> CreateFunction { get; set; }
        protected abstract bool SameKeyProducesSameDependency { get; }

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

        [TestMethod]
        public void ConstructsDependencyUsingLongestConstructor()
        {
            AddDependency<IMockDependency>(() => new MockDepenedency());
            AddAutoConstructingDependency<MockDepenedency, MockDepenedency>();

            var dep = Container.Get<MockDepenedency>();

            Assert.AreNotEqual(null, dep.Inner);
        }

        [TestMethod]
        public void GetSameTypeWithoutTagsAutoConstructing()
        {
            AddDependency<IMockDependency>(() => new MockDepenedency());
            AddAutoConstructingDependency<MockDepenedency, MockDepenedency>();

            var dep1 = Container.Get<MockDepenedency>();
            var dep2 = Container.Get<MockDepenedency>();

            if (SameKeyProducesSameDependency)
                Assert.AreEqual(dep1, dep2);
            else
                Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithoutTagsAutoConstructingShorthand()
        {
            AddDependency<IMockDependency>(() => new MockDepenedency());
            AddAutoConstructingDependency<MockDepenedency>();

            var dep1 = Container.Get<MockDepenedency>();
            var dep2 = Container.Get<MockDepenedency>();

            if (SameKeyProducesSameDependency)
                Assert.AreEqual(dep1, dep2);
            else
                Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredExceptionAutoConstructing()
        {
            AddAutoConstructingDependency<MockDepenedency, MockDepenedency>();

            Container.Get<MockDepenedency>();
        }
    }
}
