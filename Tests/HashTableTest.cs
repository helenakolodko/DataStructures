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
        public void Enumerate()
        {
            HashTable<int, string> table = new HashTable<int, string>();
            table.Add(1, "first ");
            table.Add(2, "second ");
            table.Add(3, "third");

            string result = string.Empty;
            foreach (var value in table)
                result += value;

            Assert.AreEqual("Hello to Everyone", result);
        }
    }
}
