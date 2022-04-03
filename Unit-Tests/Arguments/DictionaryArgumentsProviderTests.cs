using DI_Lite.Arguments.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit_Tests.Arguments
{
    [TestClass]
    public class DictionaryArgumentsProviderGetGenericTests : DictionaryArgumentsProviderGetTestsBase
    {
        protected override T Get<T>(string name)
            => _provider.Get<T>(name);
    }

    [TestClass]
    public class DictionaryArgumentsProviderGetTypeTests : DictionaryArgumentsProviderGetTestsBase
    {
        protected override T Get<T>(string name)
            => (T)_provider.Get(typeof(T), name);
    }

    public abstract class DictionaryArgumentsProviderGetTestsBase : DictionaryArgumentsProviderTestsBase
    {
        protected abstract T Get<T>(string name);

        [TestMethod]
        public void Get_String_ReturnsString()
        {
            var result = Get<string>(STRING_ENTRY_NAME);

            var expected = _dictionary[STRING_ENTRY_NAME];
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_Int_ReturnsInt()
        {
            var result = Get<int>(INT_ENTRY_NAME);

            var expected = int.Parse(_dictionary[INT_ENTRY_NAME]);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_Double_ReturnsDouble()
        {
            var result = Get<double>(DOUBLE_ENTRY_NAME);

            var expected = double.Parse(_dictionary[DOUBLE_ENTRY_NAME].Replace('.', ','));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_Bool_ReturnsBool()
        {
            var result = Get<bool>(BOOL_ENTRY_NAME);

            var expected = bool.Parse(_dictionary[BOOL_ENTRY_NAME]);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_Guid_ReturnsGuid()
        {
            var result = Get<Guid>(GUID_ENTRY_NAME);

            var expected = Guid.Parse(_dictionary[GUID_ENTRY_NAME]);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_DateTime_ReturnsDateTime()
        {
            var result = Get<DateTime>(DATETIME_ENTRY_NAME);

            var expected = DateTime.Parse(_dictionary[DATETIME_ENTRY_NAME]);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_GetNotExistingEntry_ThrowsKeyNotFoundException()
        {
            void act() => Get<DateTime>("");

            Assert.ThrowsException<KeyNotFoundException>(act);
        }

        [TestMethod]
        public void Get_GetNotConvertableEntry_Throws()
        {
            void act() => Get<DateTime>(INT_ENTRY_NAME);

            Assert.ThrowsException<FormatException>(act);
        }
    }

    [TestClass]
    public class DictionaryArgumentsProviderContainsGenericTests : DictionaryArgumentsProviderContainsTestsBase
    {
        protected override bool Contains<T>(string name)
            => _provider.Contains<T>(name);
    }

    [TestClass]
    public class DictionaryArgumentsProviderContainsTypeTests : DictionaryArgumentsProviderContainsTestsBase
    {
        protected override bool Contains<T>(string name)
            => _provider.Contains(typeof(T), name);
    }

    public abstract class DictionaryArgumentsProviderContainsTestsBase : DictionaryArgumentsProviderTestsBase
    {
        protected abstract bool Contains<T>(string name);

        [TestMethod]
        public void Contains_EntryExists_ReturnsTrue()
        {
            var results = new bool[] {
                Contains<string>(GUID_ENTRY_NAME),
                Contains<string>(DATETIME_ENTRY_NAME),
                Contains<string>(STRING_ENTRY_NAME),
                Contains<string>(INT_ENTRY_NAME),
                Contains<string>(DOUBLE_ENTRY_NAME),
                Contains<string>(BOOL_ENTRY_NAME),
                Contains<double>(INT_ENTRY_NAME),
                Contains<double>(DOUBLE_ENTRY_NAME),
                Contains<float>(INT_ENTRY_NAME),
                Contains<float>(DOUBLE_ENTRY_NAME),
                Contains<int>(INT_ENTRY_NAME),
                Contains<bool>(BOOL_ENTRY_NAME),
                Contains<Guid>(GUID_ENTRY_NAME),
                Contains<DateTime>(DATETIME_ENTRY_NAME)
            };

            var trues = results.Select(x => true).ToArray();
            CollectionAssert.AreEqual(results, trues);
        }

        [TestMethod]
        public void Contains_EntryDoesNotExist_ReturnsFalse()
        {
            var nonExistingEntryName = "non-existing-entry-name";

            var results = new bool[] {
                Contains<Guid>(DATETIME_ENTRY_NAME),
                Contains<Guid>(STRING_ENTRY_NAME),
                Contains<Guid>(INT_ENTRY_NAME),
                Contains<Guid>(DOUBLE_ENTRY_NAME),
                Contains<Guid>(BOOL_ENTRY_NAME),
                Contains<DateTime>(GUID_ENTRY_NAME),
                Contains<DateTime>(STRING_ENTRY_NAME),
                Contains<DateTime>(INT_ENTRY_NAME),
                Contains<DateTime>(DOUBLE_ENTRY_NAME),
                Contains<DateTime>(BOOL_ENTRY_NAME),
                Contains<double>(GUID_ENTRY_NAME),
                Contains<double>(DATETIME_ENTRY_NAME),
                Contains<double>(STRING_ENTRY_NAME),
                Contains<double>(BOOL_ENTRY_NAME),
                Contains<int>(GUID_ENTRY_NAME),
                Contains<int>(DATETIME_ENTRY_NAME),
                Contains<int>(STRING_ENTRY_NAME),
                Contains<int>(DOUBLE_ENTRY_NAME),
                Contains<int>(BOOL_ENTRY_NAME),
                Contains<bool>(GUID_ENTRY_NAME),
                Contains<bool>(DATETIME_ENTRY_NAME),
                Contains<bool>(STRING_ENTRY_NAME),
                Contains<bool>(INT_ENTRY_NAME),
                Contains<bool>(DOUBLE_ENTRY_NAME),
                Contains<Exception>(GUID_ENTRY_NAME),
                Contains<Exception>(DATETIME_ENTRY_NAME),
                Contains<Exception>(STRING_ENTRY_NAME),
                Contains<Exception>(INT_ENTRY_NAME),
                Contains<Exception>(DOUBLE_ENTRY_NAME),
                Contains<Exception>(BOOL_ENTRY_NAME),
                Contains<Guid>(nonExistingEntryName),
                Contains<DateTime>(nonExistingEntryName),
                Contains<string>(nonExistingEntryName),
                Contains<int>(nonExistingEntryName),
                Contains<double>(nonExistingEntryName),
                Contains<bool>(nonExistingEntryName),
            };

            var falses = results.Select(x => false).ToArray();
            CollectionAssert.AreEqual(results, falses);
        }
    }

    public abstract class DictionaryArgumentsProviderTestsBase
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
