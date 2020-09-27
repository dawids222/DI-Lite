using DI_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unit_Tests.Models;

namespace Unit_Tests
{
    [TestClass]
    public class ContainerGetSingleTest : ContainerBaseTest
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
            Container.Single(CreateFunction);

            var dep1 = Container.Get<IMockDependency>();
            var dep2 = Container.Get<IMockDependency>();

            Assert.AreEqual(dep1, dep2);
        }

        [TestMethod]
        public void ReturnsSameValueWhenWithTag()
        {
            Container.Single("tag", CreateFunction);

            var dep1 = Container.Get<IMockDependency>("tag");
            var dep2 = Container.Get<IMockDependency>("tag");

            Assert.AreEqual(dep1, dep2);
        }

        [TestMethod]
        public void ReturnsDifferentValuesWhenWithDifferentTags()
        {
            Container.Single("tag", CreateFunction);
            Container.Single("gat", CreateFunction);

            var dep1 = Container.Get<IMockDependency>("tag");
            var dep2 = Container.Get<IMockDependency>("gat");

            Assert.AreNotEqual(dep1, dep2);
        }

        [TestMethod]
        public void ReturnsDifferentValuesWhenWithDifferentTypes()
        {
            Container.Single(CreateFunction);
            Container.Single(() => "");

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
