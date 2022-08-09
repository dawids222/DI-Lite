using LibLite.DI.Lite;
using LibLite.DI.Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LibLite.DI.Lite.Tests.Models;

namespace LibLite.DI.Lite.Tests
{
    [TestClass]
    public class ContainerGetSingleTest : ContainerGetDependencyBaseTest
    {
        protected override bool SameKeyProducesSameDependency => true;

        protected override T Get<T>(object tag) => (T)Container.Get(typeof(T), tag);

        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.Single(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.Single(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.Single(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.Single(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.Single<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.Single<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.Single<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.Single<T>();
    }

    [TestClass]
    public class ContainerGetGenericSingleTest : ContainerGetSingleTest
    {
        protected override T Get<T>(object tag) => Container.Get<T>(tag);
    }

    [TestClass]
    public class ContainerGetFactoryTest : ContainerGetDependencyBaseTest
    {
        protected override bool SameKeyProducesSameDependency => false;

        protected override T Get<T>(object tag) => (T)Container.Get(typeof(T), tag);

        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.Factory(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.Factory(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.Factory(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.Factory(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.Factory<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.Factory<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.Factory<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.Factory<T>();
    }

    [TestClass]
    public class ContainerGetGenericFactoryTest : ContainerGetFactoryTest
    {
        protected override T Get<T>(object tag) => Container.Get<T>(tag);
    }

    [TestClass]
    public abstract class ContainerGetDependencyBaseTest : ContainerAddDependencyAbstractionBaseTest
    {
        private Func<IMockDependency> CreateFunction { get; set; }
        protected abstract bool SameKeyProducesSameDependency { get; }

        protected abstract T Get<T>(object tag = null);

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

            var dep1 = Get<IMockDependency>();
            var dep2 = Get<IMockDependency>();

            AssertEqualBaseOnSameKeyProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithSameTags()
        {
            AddDependency("tag", CreateFunction);

            var dep1 = Get<IMockDependency>("tag");
            var dep2 = Get<IMockDependency>("tag");

            AssertEqualBaseOnSameKeyProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithDifferentTags()
        {
            AddDependency("tag", CreateFunction);
            AddDependency("gat", CreateFunction);

            var dep1 = Get<IMockDependency>("tag");
            var dep2 = Get<IMockDependency>("gat");

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void GetDifferentTypesWithoutTags()
        {
            AddDependency(CreateFunction);
            AddDependency(() => "");

            var dep1 = Get<IMockDependency>();
            var dep2 = Get<string>();

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredException()
        {
            Get<IMockDependency>();
        }

        [TestMethod]
        public void ConstructsDependencyUsingTheConstructor()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency, ValidMockDependency>();

            var inner = Get<IMockDependency>();
            var outer = Get<ValidMockDependency>();

            Assert.AreEqual(null, inner.Inner);
            Assert.AreNotEqual(null, outer.Inner);
        }

        [TestMethod]
        public void GetSameTypeWithoutTagsAutoConstructing()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency, ValidMockDependency>();

            var dep1 = Get<ValidMockDependency>();
            var dep2 = Get<ValidMockDependency>();

            AssertEqualBaseOnSameKeyProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithoutTagsAutoConstructingShorthand()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency>();

            var dep1 = Get<ValidMockDependency>();
            var dep2 = Get<ValidMockDependency>();

            AssertEqualBaseOnSameKeyProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredExceptionAutoConstructing()
        {
            AddAutoConstructingDependency<ValidMockDependency>();

            Get<ValidMockDependency>();
        }

        [TestMethod]
        public void GetNestedDependenciesRegisteredInLogicalOrder()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddDependency<IMockDependency>("", () => new ValidMockDependency(Get<IMockDependency>()));

            var dependency = Get<IMockDependency>("");

            Assert.AreNotEqual(null, dependency.Inner);
            Assert.AreEqual(null, dependency.Inner.Inner);
        }

        [TestMethod]
        public void GetNestedDependenciesRegisteredInNotLogicalOrder()
        {
            AddDependency<IMockDependency>("", () => new ValidMockDependency(Get<IMockDependency>()));
            AddDependency<IMockDependency>(() => new ValidMockDependency());

            var dependency = Get<IMockDependency>("");

            Assert.AreNotEqual(null, dependency.Inner);
            Assert.AreEqual(null, dependency.Inner.Inner);
        }

        [TestMethod]
        public void ThrowsDependencyNotRegisteredWhenThereIsNoDependencyWithTag()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidDependencyWithTag>();

            void act() => Get<ValidDependencyWithTag>();

            Assert.ThrowsException<DependencyNotRegisteredException>(act);
        }

        [TestMethod]
        public void GetsDependewncyWithRequiredTag()
        {
            AddDependency<IMockDependency>(ValidDependencyWithTag.TAG, () => new ValidMockDependency());
            AddAutoConstructingDependency<ValidDependencyWithTag>();

            var dependency = Get<ValidDependencyWithTag>();

            Assert.IsNotNull(dependency);
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
        public void ThrowsExceptionWhenGettingGenericScopedDepenedencyFromContainer()
        {
            Container.Scoped(() => "TEST");

            string act() => Container.Get<string>();

            Assert.ThrowsException<DependencyRetrievalRequiresScopeException>(act);
        }

        [TestMethod]
        public void ThrowsExceptionWhenGettingScopedDepenedencyFromContainer()
        {
            Container.Scoped(() => "TEST");

            string act() => (string)Container.Get(typeof(string));

            Assert.ThrowsException<DependencyRetrievalRequiresScopeException>(act);
        }
    }
}
