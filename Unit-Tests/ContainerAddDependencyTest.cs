using LibLite.DI.Lite.Exceptions;
using LibLite.DI.Lite.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LibLite.DI.Lite.Tests
{
    [TestClass]
    public class ContainerAddDependencyStandardSingleTest : ContainerAddDependencyStandardBaseTest
    {
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
    public class ContainerAddDependencyTrySingleTest : ContainerAddDependencyTryBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.TrySingle(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.TrySingle(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.TrySingle(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.TrySingle(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.TrySingle<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.TrySingle<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.TrySingle<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.TrySingle<T>();
    }

    [TestClass]
    public class ContainerAddDependencyForceSingleTest : ContainerAddDependencyForceBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator) => Container.ForceSingle(tag, creator);
        protected override void AddDependency<T>(Func<T> creator) => Container.ForceSingle(creator);
        protected override void AddDependency<T>(object tag, Func<IDependencyProvider, T> creator) => Container.ForceSingle(tag, creator);
        protected override void AddDependency<T>(Func<IDependencyProvider, T> creator) => Container.ForceSingle(creator);
        protected override void AddAutoConstructingDependency<T, R>(object tag) => Container.ForceSingle<T, R>(tag);
        protected override void AddAutoConstructingDependency<T, R>() => Container.ForceSingle<T, R>();
        protected override void AddAutoConstructingDependency<T>(object tag) => Container.ForceSingle<T>(tag);
        protected override void AddAutoConstructingDependency<T>() => Container.ForceSingle<T>();
    }

    [TestClass]
    public class ContainerAddDependencyStandardFactoryTest : ContainerAddDependencyStandardBaseTest
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
    public class ContainerAddDependencyTryFactoryTest : ContainerAddDependencyTryBaseTest
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
    public class ContainerAddDependencyForceFactoryTest : ContainerAddDependencyForceBaseTest
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
    public class ContainerAddDependencyStandardScopedTest : ContainerAddDependencyStandardBaseTest
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
    public class ContainerAddDependencyTryScopedTest : ContainerAddDependencyBaseTest
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
    public class ContainerAddDependencyForceScopedTest : ContainerAddDependencyBaseTest
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
    public abstract class ContainerAddDependencyStandardBaseTest : ContainerAddDependencyBaseTest
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
    public abstract class ContainerAddDependencyTryBaseTest : ContainerAddDependencyBaseTest
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
    public abstract class ContainerAddDependencyForceBaseTest : ContainerAddDependencyBaseTest
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
    public class ContainerAddDependencyInstanceSingleTest : ContainerAddDependencyInstanceBaseTest
    {
        protected override void AddDependency<T>(object tag, T instance) => Container.Single(tag, instance);
        protected override void AddDependency<T>(T instance) => Container.Single(instance);
    }

    [TestClass]
    public class ContainerAddDependencyInstanceTrySingleTest : ContainerAddDependencyInstanceBaseTest
    {
        protected override void AddDependency<T>(object tag, T instance) => Container.TrySingle(tag, instance);
        protected override void AddDependency<T>(T instance) => Container.TrySingle(instance);
    }

    [TestClass]
    public class ContainerAddDependencyInstanceForceSingleTest : ContainerAddDependencyInstanceBaseTest
    {
        protected override void AddDependency<T>(object tag, T instance) => Container.ForceSingle(tag, instance);
        protected override void AddDependency<T>(T instance) => Container.ForceSingle(instance);
    }

    [TestClass]
    public abstract class ContainerAddDependencyInstanceBaseTest : ContainerBaseTest
    {
        protected abstract void AddDependency<T>(object tag, T instance);
        protected abstract void AddDependency<T>(T instance);

        [TestMethod]
        public void AddsDependencyWithDefaultKey_FromInstance()
        {
            var instance = "";
            AddDependency(instance);

            var key = new DependencyKey(typeof(string), null);
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
        }

        [TestMethod]
        public void AddsDependencyWithSpecifiedKey_FromInstance()
        {
            var instance = "";
            AddDependency("key", instance);

            var key = new DependencyKey(typeof(string), "key");
            Assert.AreEqual(key, Container.Dependencies.ElementAt(0).Key);
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
        public void AddsAutoConstructingDependencyWithUseConstructor()
        {
            AddAutoConstructingDependency<ValidUseConstructorMockDependency>();

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

        [TestMethod]
        [ExpectedException(typeof(DependencyHasMultipleUseConstructorAttributesException))]
        public void ThrowsDependencyHasMultipleUseConstructorAttributesExceptionWhileAutoConstructing()
        {
            AddAutoConstructingDependency<InvalidUseConstructorMockDependency>();
        }
    }
}
