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
    public class ContainerTrySingleTest : ContainerTryAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.TrySingle(tag, creator);
        }

        protected override void AddAutoConstructingDependency<T, R>(object tag)
        {
            Container.TrySingle<T, R>(tag);
        }
    }

    [TestClass]
    public class ContainerForceSingleTest : ContainerForceAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.ForceSingle(tag, creator);
        }

        protected override void AddAutoConstructingDependency<T, R>(object tag)
        {
            Container.ForceSingle<T, R>(tag);
        }
    }

    [TestClass]
    public class ContainerFactoryTest : ContainerStandardAddDepenedencyBaseTest
    {
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
    public class ContainerTryFactoryTest : ContainerTryAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.TryFactory(tag, creator);
        }

        protected override void AddAutoConstructingDependency<T, R>(object tag)
        {
            Container.TryFactory<T, R>(tag);
        }
    }

    [TestClass]
    public class ContainerForceFactoryTest : ContainerForceAddDepenedencyBaseTest
    {
        protected override void AddDependency<T>(object tag, Func<T> creator)
        {
            Container.ForceFactory(tag, creator);
        }

        protected override void AddAutoConstructingDependency<T, R>(object tag)
        {
            Container.ForceFactory<T, R>(tag);
        }
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
