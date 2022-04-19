﻿using DI_Lite.Arguments.Contracts;
using DI_Lite.Arguments.Models;
using DI_Lite.Arguments.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Unit_Tests.Arguments
{
    [TestClass]
    public class AggregatedArgumentsProviderGetTests : AggregatedArgumentsProviderTestsBase
    {
        protected object Get(ArgumentInfo info) => _provider.Get(info);

        [TestMethod]
        public void Get_FirstProviderReturnsValue_ReturnsValue()
        {
            var result = Get(MockInfo<string>(STRING_ENTRY_NAME));

            Assert.AreEqual(_mockEntries1[0].Value, result);
        }

        [TestMethod]
        public void Get_SecondProviderReturnsValue_ReturnsValue()
        {
            var result = Get(MockInfo<bool>(BOOL_ENTRY_NAME));

            Assert.AreEqual(_mockEntries2[0].Value, result);
        }

        [TestMethod]
        public void Get_NoneProviderReturnsValue_Throws()
        {
            void act() => Get(MockInfo<AggregatedArgumentsProviderGetTests>());

            Assert.ThrowsException<InvalidOperationException>(act);
        }
    }

    [TestClass]
    public class AggregatedArgumentsProviderContainsTests : AggregatedArgumentsProviderTestsBase
    {
        protected bool Contains(ArgumentInfo info) => _provider.Contains(info);

        [TestMethod]
        public void Contains_AnyProviderReturnsTrue_ReturnsTrue()
        {
            var results = new bool[]
            {
                Contains(MockInfo<string>(STRING_ENTRY_NAME)),
                Contains(MockInfo<int>(INT_ENTRY_NAME)),
                Contains(MockInfo<bool>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<DateTime>(DATETIME_ENTRY_NAME)),
            };

            var trues = results.Select(x => true).ToArray();
            CollectionAssert.AreEqual(results, trues);
        }

        [TestMethod]
        public void ContainsAllProvidersReturnFalse_ReturnsFalse()
        {
            var results = new bool[]
            {
                Contains(MockInfo<string>(INT_ENTRY_NAME)),
                Contains(MockInfo<int>(STRING_ENTRY_NAME)),
                Contains(MockInfo<bool>(DATETIME_ENTRY_NAME)),
                Contains(MockInfo<DateTime>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<double>("value")),
                Contains(MockInfo<AggregatedArgumentsProviderContainsTests>("??")),
            };

            var falses = results.Select(x => false).ToArray();
            CollectionAssert.AreEqual(results, falses);
        }
    }

    public abstract class AggregatedArgumentsProviderTestsBase : ArgumentsTestsBase
    {
        protected const string DATETIME_ENTRY_NAME = "datetime";
        protected const string STRING_ENTRY_NAME = "string";
        protected const string INT_ENTRY_NAME = "int";
        protected const string BOOL_ENTRY_NAME = "bool";

        private Mock<IArgumentsProvider> _providerMock1;
        private Mock<IArgumentsProvider> _providerMock2;

        protected readonly ProviderEntry[] _mockEntries1 = new ProviderEntry[]
        {
            new ProviderEntry(typeof(string), STRING_ENTRY_NAME, "TestName"),
            new ProviderEntry(typeof(int), INT_ENTRY_NAME, 25),
        };

        protected readonly ProviderEntry[] _mockEntries2 = new ProviderEntry[]
        {
            new ProviderEntry(typeof(bool), BOOL_ENTRY_NAME, true),
            new ProviderEntry(typeof(DateTime), DATETIME_ENTRY_NAME, DateTime.Parse("1996-12-21 08:00:00")),
        };

        protected AggregatedArgumentsProvider _provider;

        [TestInitialize]
        public void Before()
        {
            _providerMock1 = new();
            _providerMock2 = new();

            foreach (var entry in _mockEntries1)
            {
                _providerMock1
                    .Setup(x => x.Contains(MockInfo(entry.Type, entry.Name)))
                    .Returns(true);
                _providerMock1
                    .Setup(x => x.Get(MockInfo(entry.Type, entry.Name)))
                    .Returns(entry.Value);
            }
            foreach (var entry in _mockEntries2)
            {
                _providerMock2
                    .Setup(x => x.Contains(MockInfo(entry.Type, entry.Name)))
                    .Returns(true);
                _providerMock2
                    .Setup(x => x.Get(MockInfo(entry.Type, entry.Name)))
                    .Returns(entry.Value);
            }

            var providers = new IArgumentsProvider[]
            {
                _providerMock1.Object,
                _providerMock2.Object,
            };
            _provider = new(providers);
        }

        protected class ProviderEntry
        {
            public Type Type { get; }
            public string Name { get; }
            public object Value { get; }

            public ProviderEntry(Type type, string name, object value)
            {
                Type = type;
                Name = name;
                Value = value;
            }
        }
    }
}
