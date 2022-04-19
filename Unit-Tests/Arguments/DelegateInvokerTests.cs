using DI_Lite.Arguments;
using DI_Lite.Arguments.Contracts;
using DI_Lite.Arguments.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Unit_Tests.Arguments
{
    [TestClass]
    public class DelegateInvokerTests : ArgumentsTestsBase
    {
        private Mock<IArgumentsProvider> _providerMock;

        private DelegateInvoker _invoker;

        [TestInitialize]
        public void Before()
        {
            _providerMock = new();
        }

        [TestMethod]
        public async Task InvokeAsync_AsyncDelegate_ExecutesDelegate()
        {
            _providerMock.Setup(x => x.Get(MockInfo<string>("name"))).Returns("Bob");
            var del = async (string name) => await Task.FromResult($"Hello {name}!");
            InitInvoker(del);

            var result = await _invoker.InvokeAsync();

            Assert.AreEqual("Hello Bob!", result);
        }

        [TestMethod]
        public async Task InvokeAsync_SyncDelegate_ExecutesDelegate()
        {
            _providerMock.Setup(x => x.Get(MockInfo<Guid>("id"))).Returns(Guid.Parse("7fa48050-2ab5-462d-829b-450d7da1461e"));
            _providerMock.Setup(x => x.Get(MockInfo<int>("age"))).Returns(25);
            var del = (Guid id, int age) => $"Hi user {id}. You are {age} years old";
            InitInvoker(del);

            var result = await _invoker.InvokeAsync();

            Assert.AreEqual("Hi user 7fa48050-2ab5-462d-829b-450d7da1461e. You are 25 years old", result);
        }

        [TestMethod]
        public void InvokeAsync_DelegateThrows_ThrowsTheSameException()
        {
            var exception = new Exception("Error!");
            _providerMock.Setup(x => x.Get(It.IsAny<ArgumentInfo>())).Throws(exception);
            var del = (string name) => "";
            InitInvoker(del);

            Task act() => _invoker.InvokeAsync();

            Assert.ThrowsExceptionAsync<Exception>(act, exception.Message);
        }

        [TestMethod]
        public void Invoke_SyncDelegate_ExecutesDelegate()
        {
            _providerMock.Setup(x => x.Get(MockInfo<string>("name"))).Returns("Bob");
            var del = (string name) => $"Hello {name}!";
            InitInvoker(del);

            var result = _invoker.Invoke();

            Assert.AreEqual("Hello Bob!", result);
        }

        [TestMethod]
        public void Invoke_AsyncDelegate_ReturnsTask()
        {
            _providerMock.Setup(x => x.Get(MockInfo<Guid>("id"))).Returns(Guid.Parse("7fa48050-2ab5-462d-829b-450d7da1461e"));
            _providerMock.Setup(x => x.Get(MockInfo<int>("age"))).Returns(25);
            var del = async (Guid id, int age) => await Task.FromResult($"Hi user {id}. You are {age} years old");
            InitInvoker(del);

            var result = _invoker.Invoke();

            Assert.IsTrue(result is Task);
        }

        [TestMethod]
        public void Invoke_DelegateThrows_ThrowsTheSameException()
        {
            var exception = new Exception("Error!");
            _providerMock.Setup(x => x.Get(It.IsAny<ArgumentInfo>())).Throws(exception);
            var del = (string name) => "";
            InitInvoker(del);

            void act() => _invoker.Invoke();

            Assert.ThrowsException<Exception>(act, exception.Message);
        }

        private void InitInvoker(Delegate del)
        {
            _invoker = new(del, _providerMock.Object);

        }
    }
}
