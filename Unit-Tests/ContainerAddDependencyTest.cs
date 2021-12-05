using DI_Lite;
using DI_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerSingleTest : ContainerStandardAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.Single(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.Single(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.Single(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.Single(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.Single<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.Single<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.Single<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.Single<T>();

        [TestMethod]
        public void AddsDependencyWithDefaultKey_FromInstance()
        {
            var instance = "";
            Container.Single(instance);

            var key = new DependencyKey(typeof(string), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void AddsDependencyWithSpecifiedKey_FromInstance()
        {
            var instance = "";
            Container.Single("key", instance);

            var key = new DependencyKey(typeof(string), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }
    }

    [TestClass]
    public class ContainerTrySingleTest : ContainerTryAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.TrySingle(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.TrySingle(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.TrySingle(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.TrySingle(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.TrySingle<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.TrySingle<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.TrySingle<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.TrySingle<T>();

        [TestMethod]
        public void AddsDependencyWithDefaultKey_FromInstance()
        {
            var instance = "";
            Container.TrySingle(instance);

            var key = new DependencyKey(typeof(string), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void AddsDependencyWithSpecifiedKey_FromInstance()
        {
            var instance = "";
            Container.TrySingle("key", instance);

            var key = new DependencyKey(typeof(string), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }
    }

    [TestClass]
    public class ContainerForceSingleTest : ContainerForceAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.ForceSingle(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.ForceSingle(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.ForceSingle(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.ForceSingle(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.ForceSingle<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.ForceSingle<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.ForceSingle<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.ForceSingle<T>();


        [TestMethod]
        public void AddsDependencyWithDefaultKey_FromInstance()
        {
            var instance = "";
            Container.ForceSingle(instance);

            var key = new DependencyKey(typeof(string), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void AddsDependencyWithSpecifiedKey_FromInstance()
        {
            var instance = "";
            Container.ForceSingle("key", instance);

            var key = new DependencyKey(typeof(string), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }
    }

    [TestClass]
    public class ContainerFactoryTest : ContainerStandardAddDepenedencyBaseTest
    {
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
    public class ContainerTryFactoryTest : ContainerTryAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.TryFactory(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.TryFactory(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.TryFactory(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.TryFactory(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.TryFactory<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.TryFactory<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.TryFactory<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.TryFactory<T>();
    }

    [TestClass]
    public class ContainerForceFactoryTest : ContainerForceAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.ForceFactory(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.ForceFactory(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.ForceFactory(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.ForceFactory(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.ForceFactory<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.ForceFactory<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.TryFactory<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.ForceFactory<T>();
    }

    [TestClass]
    public class ContainerScopedTest : ContainerStandardAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.Scoped(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.Scoped(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.Scoped(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.Scoped(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.Scoped<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.Scoped<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.Scoped<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.Scoped<T>();
    }

    [TestClass]
    public class ContainerTryScopedTest : ContainerAddDependencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.TryScoped(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.TryScoped(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.TryScoped(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.TryScoped(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.TryScoped<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.TryScoped<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.TryScoped<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.TryScoped<T>();
    }

    [TestClass]
    public class ContainerForceScopedTest : ContainerAddDependencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.ForceScoped(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.ForceScoped(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.ForceScoped(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.ForceScoped(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.ForceScoped<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.ForceScoped<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.ForceScoped<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.ForceScoped<T>();
    }

    [TestClass]
    public abstract class ContainerStandardAddDepenedencyBaseTest : ContainerAddDependencyBaseTest
    {
        [TestMethod]
        public void ThrowsWhenSameDependenciesAreBeingRegistered()
        {
            AddDependency(() => "");
            void act() => AddDependency(() => "");

            Assert.ThrowsException<DependencyAlreadyRegisteredException>(act);
        }
    }

    [TestClass]
    public abstract class ContainerTryAddDepenedencyBaseTest : ContainerAddDependencyBaseTest
    {
        [TestMethod]
        public void TakesFirstWhenSameDependenciesAreBeingRegistered()
        {
            AddDependency(() => "1");
            AddDependency(() => "2");

            var result = Container.Get<string>();

            Assert.AreEqual(result, "1");
        }
    }

    [TestClass]
    public abstract class ContainerForceAddDepenedencyBaseTest : ContainerAddDependencyBaseTest
    {
        [TestMethod]
        public void TakesSecondWhenSameDependenciesAreBeingRegistered()
        {
            AddDependency(() => "1");
            AddDependency(() => "2");

            var result = Container.Get<string>();

            Assert.AreEqual(result, "2");
        }
    }

    [TestClass]
    public abstract class ContainerAddDependencyBaseTest : ContainerAddDependencyAbstractionBaseTest
    {
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
        public void AddsDependency_WithProvider()
        {
            AddDependency((provider) => "");

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsDependencyWithDefaultKey_WithProvider()
        {
            AddDependency((provider) => "");

            var key = new DependencyKey(typeof(string), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void AddsDependencyWithSpecifiedKey_WithProvider()
        {
            AddDependency("key", (provider) => "");

            var key = new DependencyKey(typeof(string), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void AddsMultipleDifferentDependenciesWithSameTag_WithProvider()
        {
            AddDependency((provider) => "1");
            AddDependency((provider) => 1);
            AddDependency((provider) => 1.0);
            AddDependency((provider) => '1');
            AddDependency((provider) => true);

            Assert.AreEqual(5, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsMultipleDifferentDependenciesWithSameType_WithProvider()
        {
            AddDependency("1", (provider) => "");
            AddDependency("2", (provider) => "");
            AddDependency(new object(), (provider) => "");
            AddDependency(this, (provider) => "");
            AddDependency(12, (provider) => "");

            Assert.AreEqual(5, Container.Dependencies.Count());
        }

        [TestMethod]
        public void PassesCorrectProviderToScopedDependency()
        {
            IDependencyProvider actual = null;
            AddDependency(provider =>
            {
                actual = provider;
                return "";
            });

            var scope = Container.CreateScope();
            scope.Get<string>();

            Assert.AreEqual(scope, actual);
        }

        [TestMethod]
        public void AddsAutoConstructingDependency()
        {
            AddAutoConstructingDependency<IMockDependency, ValidConstructorlessMockDependency>();

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsAutoConstructingDependencyWithShorthand()
        {
            AddAutoConstructingDependency<ValidConstructorlessMockDependency>();

            Assert.AreEqual(1, Container.Dependencies.Count());
        }

        [TestMethod]
        public void AddsAutoConstructingDependencyWithDefaultKey()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<ValidMockDependency, ValidMockDependency>();

            var key = new DependencyKey(typeof(ValidMockDependency), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(1).Key);
        }

        [TestMethod]
        public void AddsAutoConstructingDependencyWithSpecifiedKey()
        {
            AddDependency<IMockDependency>(() => new ValidMockDependency());
            AddAutoConstructingDependency<IMockDependency, ValidMockDependency>("key");

            var key = new DependencyKey(typeof(IMockDependency), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(1).Key);
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyHasMultipleConstructorsException))]
        public void ThrowsDependencyHasMultipleConstructorsExceptionWhileAutoConstructing()
        {
            AddAutoConstructingDependency<InvalidMockDependency>();
        }

        [TestMethod]
        [ExpectedException(typeof(DependencyHasNoConstructorException))]
        public void ThrowsDependencyHasNoConstructorExceptionWhileAutoConstructing()
        {
            AddAutoConstructingDependency<InvalidPrivateConstructorMockDependency>();
        }
    }
}
