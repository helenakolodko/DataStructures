using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;

namespace Tests
{
    [TestClass]
    public class HashTableTest
    {
        [TestMethod]
        public void Add_Get()
        {
            HashTable<int, string> table = new HashTable<int, string>();
            table.Add(1, "Hello ");
            table.Add(2, "to ");
            table.Add(3, "Everyone");

            string result = table.Get(1) + table.Get(2) + table.Get(3);

            Assert.AreEqual("Hello to Everyone", result);
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
