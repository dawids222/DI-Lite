using LibLite.DI.Lite.Arguments.Models;
using LibLite.DI.Lite.Arguments.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibLite.DI.Lite.Tests.Arguments
{
    [TestClass]
    public class DictionaryArgumentsProviderGetTests : DictionaryArgumentsProviderTestsBase
    {
        protected object Get(ArgumentInfo info) => _provider.Get(info);

        [TestMethod]
        public void Get_ArgumentInfo_ReturnsValue()
        {
            var cases = new GetTestCase[]
            {
                new GetTestCase(MockInfo<string>(STRING_ENTRY_NAME), _dictionary[STRING_ENTRY_NAME]),
                new GetTestCase(MockInfo<string>(INT_ENTRY_NAME), _dictionary[INT_ENTRY_NAME]),
                new GetTestCase(MockInfo<string>(DOUBLE_ENTRY_NAME), _dictionary[DOUBLE_ENTRY_NAME]),
                new GetTestCase(MockInfo<string>(BOOL_ENTRY_NAME), _dictionary[BOOL_ENTRY_NAME]),
                new GetTestCase(MockInfo<string>(DATETIME_ENTRY_NAME), _dictionary[DATETIME_ENTRY_NAME]),
                new GetTestCase(MockInfo<string>(GUID_ENTRY_NAME), _dictionary[GUID_ENTRY_NAME]),
                new GetTestCase(MockInfo<int>(INT_ENTRY_NAME), int.Parse(_dictionary[INT_ENTRY_NAME])),
                new GetTestCase(MockInfo<double>(DOUBLE_ENTRY_NAME), double.Parse(_dictionary[DOUBLE_ENTRY_NAME].Replace('.', ','))),
                new GetTestCase(MockInfo<double>(INT_ENTRY_NAME), double.Parse(_dictionary[INT_ENTRY_NAME])),
                new GetTestCase(MockInfo<float>(DOUBLE_ENTRY_NAME), float.Parse(_dictionary[DOUBLE_ENTRY_NAME].Replace('.', ','))),
                new GetTestCase(MockInfo<float>(INT_ENTRY_NAME), float.Parse(_dictionary[INT_ENTRY_NAME])),
                new GetTestCase(MockInfo<bool>(BOOL_ENTRY_NAME), bool.Parse(_dictionary[BOOL_ENTRY_NAME])),
                new GetTestCase(MockInfo<Guid>(GUID_ENTRY_NAME), Guid.Parse(_dictionary[GUID_ENTRY_NAME])),
                new GetTestCase(MockInfo<DateTime>(DATETIME_ENTRY_NAME), DateTime.Parse(_dictionary[DATETIME_ENTRY_NAME])),
            };

            foreach (var x in cases)
            {
                var result = Get(x.Info);

                Assert.AreEqual(x.Expected, result);
            }
        }

        [TestMethod]
        public void Get_GetNotExistingEntry_ThrowsKeyNotFoundException()
        {
            void act() => Get(MockInfo<DateTime>());

            Assert.ThrowsException<KeyNotFoundException>(act);
        }

        [TestMethod]
        public void Get_GetNotConvertableEntry_Throws()
        {
            void act() => Get(MockInfo<DateTime>(INT_ENTRY_NAME));

            Assert.ThrowsException<FormatException>(act);
        }
    }

    class GetTestCase
    {
        public ArgumentInfo Info { get; }
        public object Expected { get; }

        public GetTestCase(ArgumentInfo info, object expected)
        {
            Info = info;
            Expected = expected;
        }
    }

    [TestClass]
    public class DictionaryArgumentsProviderContainsTests : DictionaryArgumentsProviderTestsBase
    {
        protected bool Contains(ArgumentInfo info) => _provider.Contains(info);

        [TestMethod]
        public void Contains_EntryExists_ReturnsTrue()
        {
            var results = new bool[] {
                Contains(MockInfo<string>(GUID_ENTRY_NAME)),
                Contains(MockInfo<string>(DATETIME_ENTRY_NAME)),
                Contains(MockInfo<string>(STRING_ENTRY_NAME)),
                Contains(MockInfo<string>(INT_ENTRY_NAME)),
                Contains(MockInfo<string>(DOUBLE_ENTRY_NAME)),
                Contains(MockInfo<string>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<double>(INT_ENTRY_NAME)),
                Contains(MockInfo<double>(DOUBLE_ENTRY_NAME)),
                Contains(MockInfo<float>(INT_ENTRY_NAME)),
                Contains(MockInfo<float>(DOUBLE_ENTRY_NAME)),
                Contains(MockInfo<int>(INT_ENTRY_NAME)),
                Contains(MockInfo<bool>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<Guid>(GUID_ENTRY_NAME)),
                Contains(MockInfo<DateTime>(DATETIME_ENTRY_NAME)),
            };

            var trues = results.Select(x => true).ToArray();
            CollectionAssert.AreEqual(results, trues);
        }

        [TestMethod]
        public void Contains_EntryDoesNotExist_ReturnsFalse()
        {
            var nonExistingEntryName = "non-existing-entry-name";

            var results = new bool[] {
                Contains(MockInfo<Guid>(DATETIME_ENTRY_NAME)),
                Contains(MockInfo<Guid>(STRING_ENTRY_NAME)),
                Contains(MockInfo<Guid>(INT_ENTRY_NAME)),
                Contains(MockInfo<Guid>(DOUBLE_ENTRY_NAME)),
                Contains(MockInfo<Guid>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<DateTime>(GUID_ENTRY_NAME)),
                Contains(MockInfo<DateTime>(STRING_ENTRY_NAME)),
                Contains(MockInfo<DateTime>(INT_ENTRY_NAME)),
                Contains(MockInfo<DateTime>(DOUBLE_ENTRY_NAME)),
                Contains(MockInfo<DateTime>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<double>(GUID_ENTRY_NAME)),
                Contains(MockInfo<double>(DATETIME_ENTRY_NAME)),
                Contains(MockInfo<double>(STRING_ENTRY_NAME)),
                Contains(MockInfo<double>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<int>(GUID_ENTRY_NAME)),
                Contains(MockInfo<int>(DATETIME_ENTRY_NAME)),
                Contains(MockInfo<int>(STRING_ENTRY_NAME)),
                Contains(MockInfo<int>(DOUBLE_ENTRY_NAME)),
                Contains(MockInfo<int>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<bool>(GUID_ENTRY_NAME)),
                Contains(MockInfo<bool>(DATETIME_ENTRY_NAME)),
                Contains(MockInfo<bool>(STRING_ENTRY_NAME)),
                Contains(MockInfo<bool>(INT_ENTRY_NAME)),
                Contains(MockInfo<bool>(DOUBLE_ENTRY_NAME)),
                Contains(MockInfo<Exception>(GUID_ENTRY_NAME)),
                Contains(MockInfo<Exception>(DATETIME_ENTRY_NAME)),
                Contains(MockInfo<Exception>(STRING_ENTRY_NAME)),
                Contains(MockInfo<Exception>(INT_ENTRY_NAME)),
                Contains(MockInfo<Exception>(DOUBLE_ENTRY_NAME)),
                Contains(MockInfo<Exception>(BOOL_ENTRY_NAME)),
                Contains(MockInfo<Guid>(nonExistingEntryName)),
                Contains(MockInfo<DateTime>(nonExistingEntryName)),
                Contains(MockInfo<string>(nonExistingEntryName)),
                Contains(MockInfo<int>(nonExistingEntryName)),
                Contains(MockInfo<double>(nonExistingEntryName)),
                Contains(MockInfo<bool>(nonExistingEntryName)),
            };

            var falses = results.Select(x => false).ToArray();
            CollectionAssert.AreEqual(results, falses);
        }
    }

    public abstract class DictionaryArgumentsProviderTestsBase : ArgumentsTestsBase
    {
        protected const string GUID_ENTRY_NAME = "guid";
        protected const string DATETIME_ENTRY_NAME = "datetime";
        protected const string STRING_ENTRY_NAME = "string";
        protected const string INT_ENTRY_NAME = "int";
        protected const string DOUBLE_ENTRY_NAME = "double";
        protected const string BOOL_ENTRY_NAME = "bool";

        protected DictionaryArgumentsProvider _provider;
        protected readonly IDictionary<string, string> _dictionary = new Dictionary<string, string>()
        {
            { GUID_ENTRY_NAME, "29718e3f-6d71-4ab7-9c92-dc2d1c73b76b" },
            { DATETIME_ENTRY_NAME, "2015-06-19 17:35:50" }, // TODO: Take a closer look at datetime formats.
            { STRING_ENTRY_NAME, "Hello World!" },
            { INT_ENTRY_NAME, "1234" },
            { DOUBLE_ENTRY_NAME, "56.78" }, // TODO: Take a closer look at decimal designator.
            { BOOL_ENTRY_NAME, "true" },
        };

        [TestInitialize]
        public void Before()
        {
            _provider = new(_dictionary);
        }
    }
}
