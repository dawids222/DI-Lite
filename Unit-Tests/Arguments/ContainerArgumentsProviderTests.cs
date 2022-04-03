using DI_Lite;
using DI_Lite.Arguments.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Unit_Tests.Arguments
{
    [TestClass]
    public class ContainerArgumentsProviderGetGenericTests : ContainerArgumentsProviderGetTestsBase
    {
        protected override T Get<T>(string name)
            => _provider.Get<T>(name);
    }

    [TestClass]
    public class ContainerArgumentsProviderGetTypeTests : ContainerArgumentsProviderGetTestsBase
    {
        protected override T Get<T>(string name)
            => (T)_provider.Get(typeof(T), name);
    }

    public abstract class ContainerArgumentsProviderGetTestsBase : ContainerArgumentsProviderTestsBase
    {
        protected abstract T Get<T>(string name);

        [TestMethod]
        public void Get_ProviderReturnsValue_ReturnsValue()
        {
            var name = "name";
            var expected = "expected";
            _dependencyProviderMock
                .Setup(x => x.Get(typeof(string), null))
                .Returns(expected);

            var result = Get<string>(name);

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

            void act() => Get<string>(name);

            Assert.ThrowsException<Exception>(act, exception.Message);
        }
    }

    [TestClass]
    public class ContainerArgumentsProviderContainsGenericTests : ContainerArgumentsProviderContainsTestsBase
    {
        protected override bool Contains<T>(string name)
            => _provider.Contains<T>(name);
    }

    [TestClass]
    public class ContainerArgumentsProviderContainsTypeTests : ContainerArgumentsProviderContainsTestsBase
    {
        protected override bool Contains<T>(string name)
            => _provider.Contains(typeof(T), name);
    }

    public abstract class ContainerArgumentsProviderContainsTestsBase : ContainerArgumentsProviderTestsBase
    {
        protected abstract bool Contains<T>(string name);

        [TestMethod]
        public void Contains_ProviderReturnsTrue_ReturnsTrue()
        {
            var name = "name";
            var expected = true;
            _dependencyProviderMock
                .Setup(x => x.Contains(typeof(string), null))
                .Returns(expected);

            var result = Contains<string>(name);

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

            var result = Contains<string>(name);

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

            void act() => Contains<string>(name);

            Assert.ThrowsException<Exception>(act, exception.Message);
        }
    }

    public abstract class ContainerArgumentsProviderTestsBase
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
