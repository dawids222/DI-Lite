using DI_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerGetFactoryTest : ContainerBaseTest
    {
        private Func<IMockDependency> CreateFunction { get; set; }

        [TestInitialize]
        public override void Before()
        {
            base.Before();
            CreateFunction = () => new MockDepenedency();
        }

        [TestMethod]
        public void ReturnsSameValueWhenWithoutTag()
        {
            Container.Factory(CreateFunction);

            var dep1 = Container.Get<IMockDependency>();
            var dep2 = Container.Get<IMockDependency>();

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void ReturnsSameValueWhenWithTag()
        {
            Container.Factory("tag", CreateFunction);

            var dep1 = Container.Get<IMockDependency>("tag");
            var dep2 = Container.Get<IMockDependency>("tag");

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void ReturnsDifferentValuesWhenWithDifferentTags()
        {
            Container.Factory("tag", CreateFunction);
            Container.Factory("gat", CreateFunction);

            var dep1 = Container.Get<IMockDependency>("tag");
            var dep2 = Container.Get<IMockDependency>("gat");

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void ReturnsDifferentValuesWhenWithDifferentTypes()
        {
            Container.Factory(CreateFunction);
            Container.Factory(() => "");

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
