using LibLite.DI.Lite;
using LibLite.DI.Lite.Arguments.Models;
using LibLite.DI.Lite.Arguments.Providers;
using LibLite.DI.Lite.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace LibLite.DI.Lite.Tests.Arguments
{
    [TestClass]
    public class ContainerArgumentsProviderGetTests : ContainerArgumentsProviderTestsBase
    {
        protected object Get(ArgumentInfo info) => _provider.Get(info);

        [TestMethod]
        public void Get_ProviderReturnsValue_ReturnsValue()
        {
            var name = "name";
            var expected = "expected";
            _dependencyProviderMock
                .Setup(x => x.Get(typeof(string), null))
                .Returns(expected);

            var result = Get(MockInfo<string>(name));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_WithTag_ProviderReturnsValue_ReturnsValue()
        {
            var name = "name";
            var tag = "tag";
            var expected = "expected";
            _dependencyProviderMock
                .Setup(x => x.Get(typeof(string), tag))
                .Returns(expected);

            var result = Get(MockInfo<string>(name, new Attribute[] { new WithTagAttribute(tag) }));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_ProviderThrows_ThrowsTheSameException()
        {
            var name = "name";
            var exception = new Exception("Error!");
            _dependencyProviderMock
                .Setup(x => x.Get(typeof(string), null))
                .Throws(exception);

            void act() => Get(MockInfo<string>(name));

            Assert.ThrowsException<Exception>(act, exception.Message);
        }
    }

    [TestClass]
    public class ContainerArgumentsProviderContainsTests : ContainerArgumentsProviderTestsBase
    {
        protected bool Contains(ArgumentInfo info) => _provider.Contains(info);

        [TestMethod]
        public void Contains_ProviderReturnsTrue_ReturnsTrue()
        {
            var name = "name";
            var expected = true;
            _dependencyProviderMock
                .Setup(x => x.Contains(typeof(string), null))
                .Returns(expected);

            var result = Contains(MockInfo<string>(name));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Contains_WithTag_ProviderReturnsTrue_ReturnsTrue()
        {
            var name = "name";
            var tag = "tag";
            var expected = true;
            _dependencyProviderMock
                .Setup(x => x.Contains(typeof(string), tag))
                .Returns(expected);

            var result = Contains(MockInfo<string>(name, new Attribute[] { new WithTagAttribute(tag) }));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Contains_ProviderReturnsFalse_ReturnsFalse()
        {
            var name = "name";
            var expected = false;
            _dependencyProviderMock
                .Setup(x => x.Contains(typeof(string), null))
                .Returns(expected);

            var result = Contains(MockInfo<string>(name));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Contains_WithTag_ProviderReturnsFalse_ReturnsFalse()
        {
            var name = "name";
            var expected = false;
            _dependencyProviderMock
                .Setup(x => x.Contains(typeof(string), "tag"))
                .Returns(expected);

            var result = Contains(MockInfo<string>(name, new Attribute[] { new WithTagAttribute("gat") }));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Contains_ProviderThrows_ThrowsTheSameException()
        {
            var name = "name";
            var exception = new Exception("Error!");
            _dependencyProviderMock
                .Setup(x => x.Contains(typeof(string), null))
                .Throws(exception);

            void act() => Contains(MockInfo<string>(name));

            Assert.ThrowsException<Exception>(act, exception.Message);
        }
    }

    public abstract class ContainerArgumentsProviderTestsBase : ArgumentsTestsBase
    {
        protected Mock<IDependencyProvider> _dependencyProviderMock;
        protected ContainerArgumentsProvider _provider;

        [TestInitialize]
        public void Before()
        {
            _dependencyProviderMock = new();
            _provider = new(_dependencyProviderMock.Object);
        }
    }
}
