using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;

namespace Tests
{
    [TestClass]
    public class HashTableTest
    {
        [TestMethod]
        public void Add_ToHashTable_AddsAllElements()
        {
            HashTable<int, string> table = new HashTable<int, string>();

            table.Add(1, "Hello ");
            table.Add(2, "to ");
            table.Add(3, "Everyone");

            Assert.AreEqual(3, table.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_DuplicateKey_ThrowsException()
        {
            HashTable<int, string> table = new HashTable<int, string>();

            table.Add(1, "Hello ");
            table.Add(1, "duplicate");
        }
        
        [TestMethod]
        public void Get_ByKey_ReturnesCorrectValue()
        {
            HashTable<int, string> table = new HashTable<int, string>();

            table.Add(1, "Hello ");
            table.Add(2, "to ");
            table.Add(3, "Everyone");

            string result = table.Get(2);

            Assert.AreEqual("to ", result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Get_ByNonExistingKey_ThrowsException()
        {
            HashTable<int, string> table = new HashTable<int, string>();
            table.Get(1);
        }

        [TestMethod]
        public void Enumerate_OnEmptyTable_ReturnsEmptyResult()
        {
            HashTable<int, string> table = new HashTable<int, string>();

            string result = string.Empty;
            foreach (var value in table)
                result += value;

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Enumerate()
        {
            HashTable<int, string> table = new HashTable<int, string>();

            table.Add(1, "Hello ");
            table.Add(2, "to ");
            table.Add(3, "Everyone");

            string result = string.Empty;
            foreach (var value in table)
                result += value;

            Assert.AreEqual("Hello to Everyone", result);
        }
    }
}
