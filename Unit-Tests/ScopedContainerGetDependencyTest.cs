using DI_Lite;
using DI_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ScopedContainerGetSingleTest : ScopedContainerGetDependencyBaseTest
    {
        protected override bool SameKeyProducesSameDependencyForSameScope => true;

        protected override bool SameKeyProducesSameDependencyForDifferentScopes => true;

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
    public class ScopedContainerGetFactoryTest : ScopedContainerGetDependencyBaseTest
    {
        protected override bool SameKeyProducesSameDependencyForSameScope => false;

        protected override bool SameKeyProducesSameDependencyForDifferentScopes => false;

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
    public class ScopedContainerGetScopedTest : ScopedContainerGetDependencyBaseTest
    {
        protected override bool SameKeyProducesSameDependencyForSameScope => true;

        protected override bool SameKeyProducesSameDependencyForDifferentScopes => false;

        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.Scoped(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.Scoped(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.Scoped(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.Scoped(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.Scoped<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.Scoped<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.Scoped<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.Scoped<T>();

        [TestMethod]
        public void GetSameScopedDependencyWithinScope()
        {
            AddAutoConstructingDependency<IMockDependency, ValidParameterlessMockDependency>();
            AddAutoConstructingDependency<ValidMockDependency>();
            var scope = Container.CreateScope();

            var dep1 = scope.Get<IMockDependency>();
            var dep2 = scope.Get<ValidMockDependency>();

            Assert.AreEqual(dep1, dep2.Inner);
        }
    }

    [TestClass]
    public abstract class ScopedContainerGetDependencyBaseTest : ContainerAddDependencyAbstractionBaseTest
    {
        private Func<IMockDependency> CreateFunction { get; set; }
        protected abstract bool SameKeyProducesSameDependencyForSameScope { get; }
        protected abstract bool SameKeyProducesSameDependencyForDifferentScopes { get; }



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
            var scope = Container.CreateScope();

            var dep1 = scope.Get<IMockDependency>();
            var dep2 = scope.Get<IMockDependency>();

            AssertEqualBaseOnSameKeyScopeProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithSameTags()
        {
            AddDependency("tag", CreateFunction);
            var scope = Container.CreateScope();

            var dep1 = scope.Get<IMockDependency>("tag");
            var dep2 = scope.Get<IMockDependency>("tag");

            AssertEqualBaseOnSameKeyScopeProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithDifferentTags()
        {
            AddDependency("tag", CreateFunction);
            AddDependency("gat", CreateFunction);
            var scope = Container.CreateScope();

            var dep1 = scope.Get<IMockDependency>("tag");
            var dep2 = scope.Get<IMockDependency>("gat");

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void GetDifferentTypesWithoutTags()
        {
            AddDependency(CreateFunction);
            AddDependency(() => "");
            var scope = Container.CreateScope();

            var dep1 = scope.Get<IMockDependency>();
            var dep2 = scope.Get<string>();

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredException()
        {
            var scope = Container.CreateScope();

            scope.Get<IMockDependency>();
        }

        [TestMethod]
        public void ConstructsDependencyUsingTheConstructor()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency, ValidMockDependency>();
            var scope = Container.CreateScope();

            var inner = scope.Get<IMockDependency>();
            var outer = scope.Get<ValidMockDependency>();

            Assert.AreEqual(null, inner.Inner);
            Assert.AreNotEqual(null, outer.Inner);
        }

        [TestMethod]
        public void GetSameTypeWithoutTagsAutoConstructing()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency, ValidMockDependency>();
            var scope = Container.CreateScope();

            var dep1 = scope.Get<ValidMockDependency>();
            var dep2 = scope.Get<ValidMockDependency>();

            AssertEqualBaseOnSameKeyScopeProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameTypeWithoutTagsAutoConstructingShorthand()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency>();
            var scope = Container.CreateScope();

            var dep1 = scope.Get<ValidMockDependency>();
            var dep2 = scope.Get<ValidMockDependency>();

            AssertEqualBaseOnSameKeyScopeProducesSameDependency(dep1, dep2);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyNotRegisteredException))]
        public void ThrowsDependencyNotRegisteredExceptionAutoConstructing()
        {
            AddAutoConstructingDependency<ValidMockDependency>();
            var scope = Container.CreateScope();

            scope.Get<ValidMockDependency>();
        }

        [TestMethod]
        public void GetNestedDependenciesRegisteredInLogicalOrder()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            var scope = Container.CreateScope();
            AddDependency<IMockDependency>("", () => new ValidMockDependency(scope.Get<IMockDependency>()));
            scope = Container.CreateScope();

            var dependency = scope.Get<IMockDependency>("");

            Assert.AreNotEqual(null, dependency.Inner);
            Assert.AreEqual(null, dependency.Inner.Inner);
        }

        [TestMethod]
        public void GetNestedDependenciesRegisteredInNotLogicalOrder()
        {
            var scope = Container.CreateScope();
            AddDependency<IMockDependency>("", () => new ValidMockDependency(scope.Get<IMockDependency>()));
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            scope = Container.CreateScope();

            var dependency = scope.Get<IMockDependency>("");

            Assert.AreNotEqual(null, dependency.Inner);
            Assert.AreEqual(null, dependency.Inner.Inner);
        }

        [TestMethod]
        public void GetSameDependencyFromDifferentScopes()
        {
            AddDependency(null, () => new ValidMockDependency());
            var scope1 = Container.CreateScope();
            var scope2 = Container.CreateScope();

            var dep1 = scope1.Get<ValidMockDependency>();
            var dep2 = scope2.Get<ValidMockDependency>();

            AssertEqualBaseOnSameKeyScopesProduceSameDependency(dep1, dep2);
        }

        [TestMethod]
        public void GetSameDependencyFromDifferentScopesAutoConstruct()
        {
            AddAutoConstructingDependency<ValidParameterlessMockDependency>();
            var scope1 = Container.CreateScope();
            var scope2 = Container.CreateScope();

            var dep1 = scope1.Get<ValidParameterlessMockDependency>();
            var dep2 = scope2.Get<ValidParameterlessMockDependency>();

            AssertEqualBaseOnSameKeyScopesProduceSameDependency(dep1, dep2);
        }

        private void AssertEqualBaseOnSameKeyScopeProducesSameDependency<T>(T dep1, T dep2)
        {
            if (SameKeyProducesSameDependencyForSameScope)
                Assert.AreEqual(dep1, dep2);
            else
                Assert.AreNotEqual(dep1, dep2);
        }

        private void AssertEqualBaseOnSameKeyScopesProduceSameDependency<T>(T dep1, T dep2)
        {
            if (SameKeyProducesSameDependencyForDifferentScopes)
                Assert.AreEqual(dep1, dep2);
            else
                Assert.AreNotEqual(dep1, dep2);
        }
    }
}
