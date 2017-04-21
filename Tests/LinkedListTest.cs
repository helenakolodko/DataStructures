using System;
using System.Linq;
using DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class LinkedListTest
    {

        #region Constructor
        [TestMethod]
        public void Create_WithNoArgumentsProvided_CreatesEmptyList()
        {
            LinkedList<string> tree = new LinkedList<string>();

            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullArgument_ThrowsException()
        {
            string[] items = null;
            LinkedList<string> tree = new LinkedList<string>(items);
        }


        [TestMethod]
        public void Create_WithItemsProvided_AddsAllItemsToList()
        {
            LinkedList<int> tree = new LinkedList<int>(new[] { 54, 25, 316, 4, 55, 6, });

            Assert.AreEqual(6, tree.Count);
        }

        #endregion

        #region Add
        [TestMethod]
        public void Add_ToEmptyList_IncreasesCount()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Add(11);

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Add_ToNonEmptyList_IncreasesCount()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Add(11);
            list.Add(12);

            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void Add_ToFullList_ExpandsList()
        {
            LinkedList<int> list = new LinkedList<int>(new[] { 54, 25, 316, 4, 55, 6,});

            list.Add(11);
            list.Add(41);
            list.Add(111);

            Assert.AreEqual(9, list.Count);
        }
        #endregion

        #region Contains
        [TestMethod]
        public void Contains_OnEmptyList_ReturnsFalse()
        {
            LinkedList<int> list = new LinkedList<int>();

            var contains = list.Contains(12);

            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void Contains_WithExistingItem_ReturnsTrue()
        {
            LinkedList<int> list = new LinkedList<int>(new[] { 54, 25, 316, 4, 55, 6, });

            var contains = list.Contains(4);

            Assert.IsTrue(contains);
        }
        
        [TestMethod]
        public void Contains_WithNonExistingItem_ReturnsFalse()
        {
            LinkedList<int> list = new LinkedList<int>(new[] { 54, 25, 316, 4, 55, 6, });

            var contains = list.Contains(545);

            Assert.IsFalse(contains);
        }

        #endregion

        #region Insert


        #endregion

        #region Remove
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_FromEmptyList_ThrowsException()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Remove(12);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_WithNonExistingItem_ThrowsException()
        {
            LinkedList<int> list = new LinkedList<int>(new[] { 54, 25, 316, 4, 55, 6, });

            list.Remove(12);
        }

        [TestMethod]
        public void Remove_WithExistingItem_DecrementsCount()
        {
            LinkedList<int> list = new LinkedList<int>(new[] { 54, 25, 316, 4, 55, 6, });

            list.Remove(55);

            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void Remove_WithExistingItem_RemovesIt()
        {
            LinkedList<int> list = new LinkedList<int>(new[] { 54, 25, 316, 4, 55, 6, });

            list.Remove(55);
            var contains = list.Contains(55);

            Assert.IsFalse(contains);
        }
        #endregion

        #region Clear

        [TestMethod]
        public void Clear_OnEmptyList_DoesNothing()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Clear();

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Clear_OnNonEmptyList_ClearsTheList()
        {
            LinkedList<int> list = new LinkedList<int>(new[] { 54, 25, 316, 4, 55, 6, });

            list.Clear();

            Assert.AreEqual(0, list.Count);
        }

        #endregion

        #region Enumeration


        #endregion
    }
}
