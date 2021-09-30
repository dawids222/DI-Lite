using DI_Lite;
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
            CreateFunction = () => new ValidMockDependency();
        }

        [TestMethod]
        public void GetSameTypeWithoutTags()
        {
            AddDependency(CreateFunction);

            var dep1 = Container.Get<IMockDependency>();
            var dep2 = Container.Get<IMockDependency>();

            AssertEqualBaseOnSameKeyProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithSameTags()
        {
            AddDependency("tag", CreateFunction);

            var dep1 = Container.Get<IMockDependency>("tag");
            var dep2 = Container.Get<IMockDependency>("tag");

            AssertEqualBaseOnSameKeyProducesSameDependency(dep1, dep2);
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
        public void ConstructsDependencyUsingTheConstructor()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency, ValidMockDependency>();

            var inner = Container.Get<IMockDependency>();
            var outer = Container.Get<ValidMockDependency>();

            Assert.AreEqual(null, inner.Inner);
            Assert.AreNotEqual(null, outer.Inner);
        }

        [TestMethod]
        public void GetSameTypeWithoutTagsAutoConstructing()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency, ValidMockDependency>();

            var dep1 = Container.Get<ValidMockDependency>();
            var dep2 = Container.Get<ValidMockDependency>();

            AssertEqualBaseOnSameKeyProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithoutTagsAutoConstructingShorthand()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency>();

            var dep1 = Container.Get<ValidMockDependency>();
            var dep2 = Container.Get<ValidMockDependency>();

            AssertEqualBaseOnSameKeyProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredExceptionAutoConstructing()
        {
            AddAutoConstructingDependency<ValidMockDependency>();

            Container.Get<ValidMockDependency>();
        }

        [TestMethod]
        public void GetNestedDependenciesRegisteredInLogicalOrder()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddDependency<IMockDependency>("", () => new ValidMockDependency(Container.Get<IMockDependency>()));

            var dependency = Container.Get<IMockDependency>("");

            Assert.AreNotEqual(null, dependency.Inner);
            Assert.AreEqual(null, dependency.Inner.Inner);
        }

        [TestMethod]
        public void GetNestedDependenciesRegisteredInNotLogicalOrder()
        {
            AddDependency<IMockDependency>("", () => new ValidMockDependency(Container.Get<IMockDependency>()));
            AddDependency<IMockDependency>(() => new ValidMockDependency());

            var dependency = Container.Get<IMockDependency>("");

            Assert.AreNotEqual(null, dependency.Inner);
            Assert.AreEqual(null, dependency.Inner.Inner);
        }

        private void AssertEqualBaseOnSameKeyProducesSameDependency<T>(T dep1, T dep2)
        {
            if (SameKeyProducesSameDependency)
                Assert.AreEqual(dep1, dep2);
            else
                Assert.AreNotEqual(dep1, dep2);
        }
    }

    [TestClass]
    public class ContainerGetScopedTest
    {
        private Container Container { get; set; }

        [TestInitialize]
        public void Before()
        {
            Container = new Container();
        }

        [TestMethod]
        public void ThrowsExceptionWhenGettingScopedDepenedencyFromContainer()
        {
            Container.Scoped(() => "TEST");

            string act() => Container.Get<string>();

            Assert.ThrowsException<DependencyRetrievalRequireScopeException>(act);
        }
    }
}
