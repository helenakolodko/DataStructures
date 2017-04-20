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

    }
}
