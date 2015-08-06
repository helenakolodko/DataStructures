using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;
using System.Collections;

namespace Tests
{
    [TestClass]
    public class BinaryTreeTest
    {
        [TestMethod]
        public void Preorder()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new int[]{54, 25, 316, 4, 55, 6,});
            int[] actual = new int[6];
            int i = 0;
            foreach (var item in tree)
                actual[i++] = item;
        }

        [TestMethod]
        public void Inorder()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new int[] { 54, 25, 316, 4, 55, 6, });
            int[] actual = new int[6];
            int i = 0;
            foreach (var item in tree.InOrderTraversal)
                actual[i++] = item;
            IStructuralEquatable expected = new int[] { 4, 6, 25, 54, 55, 316 };
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
            
        }

        [TestMethod]
        public void Postorder()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new int[] { 54, 25, 316, 4, 55, 6, });
            int[] actual = new int[6];
            int i = 0;
            foreach (var item in tree.PostOrderTraversal)
                actual[i++] = item;

        }
    }
}
