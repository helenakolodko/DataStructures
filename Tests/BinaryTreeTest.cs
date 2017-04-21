using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;
using System.Collections;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class BinaryTreeTest
    {
        [TestMethod]
        public void Add_ToFullIntTree_ExpandsTree()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            tree.Add(11);
            tree.Add(41);
            tree.Add(111);

            Assert.AreEqual(9, tree.Count);
        }

        [TestMethod]
        public void Remove_LeafFromIntTree_RemovesIt()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            tree.Remove(55);

            Assert.AreEqual(5, tree.Count);
        }

        [TestMethod]
        public void Remove_LeafFromIntTree_SavesRightStructure()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            tree.Remove(55);
            int[] actual = tree.InOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 4, 6, 25, 54, 316 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Remove_NodeWithOneChildFromIntTree_SavesRightStructure()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            tree.Remove(4);
            int[] actual = tree.InOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 6, 25, 54, 55, 316 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Remove_NodeWithTwoChildrenFromIntTree_SavesRightStructure()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            tree.Remove(25);
            int[] actual = tree.InOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 4, 6, 54, 55, 316 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Remove_RootFromIntTree_SavesRightStructure()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            tree.Remove(54);
            int[] actual = tree.InOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 4, 6, 25, 55, 316 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_NonExistingElement_ThrowsException()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            tree.Remove(5);
        }

        [TestMethod]
        public void Contains_WithExistingElement_ReturnesTrue()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            var contains = tree.Contains(316);
            
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Contains_WithNonExistingElement_ReturnesFalse()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            var contains = tree.Contains(315);

            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void Add_ToFullIntTree_AddsItemsRight()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            tree.Add(11);
            tree.Add(41);
            tree.Add(111);
            int[] actual = tree.InOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 4, 6, 11, 25, 41, 54, 55, 111, 316 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Enumerate_OnEmptyTree_ReturnsEmptyResult()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            int[] actual = tree.ToArray();

            IStructuralEquatable expected = new int[] {  };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));

        }

        [TestMethod]
        public void PreOrderTraversal_OnIntTree_EnumeratesInCorrectOrder()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            int[] actual = tree.ToArray();

            IStructuralEquatable expected = new[] { 54, 25, 4, 6, 316, 55 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void InOrderTraversal_OnIntTree_EnumeratesInCorrectOrder()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            int[] actual = tree.InOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 4, 6, 25, 54, 55, 316 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));

        }

        [TestMethod]
        public void PostOrderTraversal_OnIntTree_EnumeratesInCorrectOrder()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, });

            int[] actual = tree.PostOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 6, 4, 25, 55, 316, 54 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Balance_OnUnbalancedTree_MakesBalanced()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 25, 316, 4, 55, 6, 75 });

            tree.Balance();
            int[] actual = tree.PostOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 4, 25, 6, 55, 316, 75, 54 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Balance_OnOneBranchTree_MakesBalanced()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 4, 6, 25, 54, 55, 75, 316 });

            tree.Balance();
            int[] actual = tree.PostOrderTraversal.ToArray();

            IStructuralEquatable expected = new[] { 4, 25, 6, 55, 316, 75, 54 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Balance_OnBalancedTree_MakesNoChanges()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new[] { 54, 6, 75, 4, 25, 55, 316 });

            IStructuralEquatable expected = tree.PostOrderTraversal.ToArray();
            tree.Balance();
            int[] actual = tree.PostOrderTraversal.ToArray();
            
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void Balance_OnEmptyTree_MakesNoChanges()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            IStructuralEquatable expected = tree.PostOrderTraversal.ToArray();
            tree.Balance();
            int[] actual = tree.PostOrderTraversal.ToArray();

            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }
    }
}
