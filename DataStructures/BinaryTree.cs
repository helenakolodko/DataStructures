using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DataStructures
{
    public class BinaryTree<T>: IEnumerable<T>
    {
        private struct Node<T>
        {
            internal T Value;
            internal int Left;
            internal int Right;
            internal int Parent;
            public Node(T value, int parent)
            {
                Value = value;
                Left = -1;
                Right = -1;
                Parent = parent;
            }
        }

        private int root;
        private Node<T>[] itemCells;
        private int count;
        private int next;
        private int[] freeCells;
        private int freeCount;
        private int capacity;
        private Comparison<T> comparison;
         
        public int Count { get { return count; } }
        public IEnumerable<T> PreOrderTraversal { get { return GetPreOrder(root); } }
        public IEnumerable<T> InOrderTraversal { get { return GetInOrder(root); } }
        public IEnumerable<T> PostOrderTraversal { get { return GetPostOrder(root); } }

        public BinaryTree(IComparer<T> comparer)
            : this(16, comparer.Compare)
        { }

        public BinaryTree(T[] items)
            : this(items.Length, null)
        {
            for (int i = 0; i < capacity; i++)
                Add(items[i]);
        }
        public BinaryTree(int capacity, Comparison<T> comparison)
        {
            this.comparison = comparison ?? Comparer<T>.Default.Compare;
            this.capacity = capacity;
            itemCells = new Node<T>[capacity];
        }

        public void Add(T value)
        {
            if (count == 0)
            {
                itemCells[0] = new Node<T>(value, -1);
                next++;
            }
            else
            {
                if (count >= capacity)
                    ExpandItemsArray();
                Insert(value);
            }
            count++;
        }

        public void Remove(T value)
        {
            int found = FindItemPosition(value);
            if (found >= 0)
            {
                if (found == root)
                    if (count == 1)
                        root = -1;
                    else if (itemCells[found].Left >= 0 ^ itemCells[found].Right >= 0)
                        root = itemCells[found].Left >= 0 ? itemCells[found].Left : itemCells[found].Right;

                Remove(found, value);
                count--;
                AddToFreeCells(found);
            }
            else
                throw new InvalidOperationException("Binary Tree doesn't contain this value.");
        }

        public bool Contains(T value)
        {
            return FindItemPosition(value) >= 0;
        }

        public void Balance()
        {
            if (root >= 0)
            {
                var orderedIndexes = GetInOrderIndexes(root).ToArray();
                root = BalanceSubtree(orderedIndexes, -1, 0, orderedIndexes.Length);
            }
        }

        private int BalanceSubtree(int[] orderedIndexes, int parent, int start, int end)
        {
            if (end > start)
            {
                var rootIndex = start + (end - start) / 2;
                var index = orderedIndexes[rootIndex];
                itemCells[index].Parent = parent;
                itemCells[index].Left = BalanceSubtree(orderedIndexes, index, start, rootIndex);
                itemCells[index].Right = BalanceSubtree(orderedIndexes, index, rootIndex + 1, end);
                return index;
            }
            else
                return -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in GetPreOrder(root))
            {
                yield return value;
            }
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Remove(int current, T value)
        {
            if (current < 0)
                return;
            
            if (itemCells[current].Left >= 0 && itemCells[current].Right >= 0)
            {
                int successor = FindMinInSubtreePosition(itemCells[current].Right);
                itemCells[current].Value = itemCells[successor].Value;
                Remove(successor, itemCells[successor].Value);
            }
            else if (itemCells[current].Left >= 0)
                ReplaceChildInParent(current, itemCells[current].Left);
            else if (itemCells[current].Right >= 0)
                ReplaceChildInParent(current, itemCells[current].Right);
            else
                ReplaceChildInParent(current, -1);
        }

        private int FindMinInSubtreePosition(int rootIndex)
        {
            int current = rootIndex;
            while (itemCells[current].Left >= 0)
                current = itemCells[current].Left;
            return current;
        }

        private void ReplaceChildInParent(int current, int newIndex)
        {
            int parent = itemCells[current].Parent;
            if (parent >= 0)
                if (itemCells[parent].Left == current)
                    itemCells[parent].Left = newIndex;
                else
                    itemCells[parent].Right = newIndex;
            if (newIndex >= 0)
                itemCells[newIndex].Parent = parent;
        }

        private void AddToFreeCells(int found)
        {
            if (freeCells == null)
                freeCells = new int[16];
            else if (freeCells.Length <= freeCount)
            {
                int[] temp = new int[freeCount*2];
                Array.Copy(freeCells, temp, freeCells.Length);
                freeCells = temp;
            }
            freeCells[freeCount] = found;
            freeCount++;
        }

        private void ExpandItemsArray()
        {
            capacity *= 2;
            Node<T>[] temp = new Node<T>[capacity];
            Array.Copy(itemCells, temp, itemCells.Length);
            itemCells = temp;
        }

        private void Insert(T value)
        {
            int parent = FindInsertPosition(value);
            int position = AddToArray(value, parent);
            if (comparison(value, itemCells[parent].Value) > 0)
                itemCells[parent].Right = position;
            else
                itemCells[parent].Left = position;
        }

        private int AddToArray(T value, int parent)
        {
            int position = -1;
            if (freeCount > 0)
                position = freeCells[freeCount-- - 1];
            else
                position = next++;
            itemCells[position] = new Node<T>(value, parent);
            return position;
        }

        private int FindItemPosition(T value)
        {
            int current = root;
            while (current >= 0)
            {
                int comparisonResult = comparison(value, itemCells[current].Value);
                if (comparisonResult == 0)
                    return current;
                if (comparisonResult > 0)
                    current = itemCells[current].Right;
                else
                    current = itemCells[current].Left;
            }
            return -1;
        }

        private int FindInsertPosition(T value)
        {
            int parent = root;
            int current = root;
            while (current >= 0)
            {
                parent = current;
                if (comparison(value, itemCells[parent].Value) > 0)
                    current = itemCells[parent].Right;
                else
                    current = itemCells[parent].Left;
            }
            return parent;
        }

        private IEnumerable<T> GetPreOrder(int subtreePosition)
        {
            if (subtreePosition != -1)
            {
                yield return itemCells[subtreePosition].Value;
                foreach (var value in GetPreOrder(itemCells[subtreePosition].Left))
                {
                    yield return value;
                }
                foreach (var value in GetPreOrder(itemCells[subtreePosition].Right))
                {
                    yield return value;
                }
            }
        }

        private IEnumerable<T> GetInOrder(int subtreePosition)
        {
            if (subtreePosition != -1)
            {
                foreach (var value in GetInOrder(itemCells[subtreePosition].Left))
                {
                    yield return value;
                }
                yield return itemCells[subtreePosition].Value;
                foreach (var value in GetInOrder(itemCells[subtreePosition].Right))
                {
                    yield return value;
                }
            }
        }

        private IEnumerable<int> GetInOrderIndexes(int subtreePosition)
        {
            if (subtreePosition != -1)
            {
                foreach (var value in GetInOrderIndexes(itemCells[subtreePosition].Left))
                {
                    yield return value;
                }
                yield return subtreePosition;
                foreach (var value in GetInOrderIndexes(itemCells[subtreePosition].Right))
                {
                    yield return value;
                }
            }
        }

        private IEnumerable<T> GetPostOrder(int subtreePosition)
        {
            if (subtreePosition != -1)
            {
                foreach (var value in GetPostOrder(itemCells[subtreePosition].Left))
                {
                    yield return value;
                }
                foreach (var value in GetPostOrder(itemCells[subtreePosition].Right))
                {
                    yield return value;
                }
                yield return itemCells[subtreePosition].Value;
            }
        }
    }
}
