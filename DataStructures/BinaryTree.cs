using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class BinaryTree<T>: IEnumerable<T>
    {
        private struct Node<T>
        {
            internal T Value;
            internal int Left;
            internal int Right;
            public Node(T value)
            {
                Value = value;
                Left = -1;
                Right = -1;
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
                itemCells[0] = new Node<T>(value);
                next++;
            }
            else if (count >= capacity)
                ExpandItemsArray();
            else
                Insert(value);
            count++;
        }

        public void Remove(T value)
        {

        }

        public bool Contains(T value)
        {
            int current = root;
            while (current > 0)
            {
                int comparisonResult = comparison(value, itemCells[current].Value);
                if (comparisonResult == 0)
                    return true;
                if (comparisonResult > 0)
                    current = itemCells[current].Right;
                else
                    current = itemCells[current].Left;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in GetPreOrder(root))
            {
                yield return value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            int position = -1;
            if (freeCount > 0)
                position = freeCells[freeCount - 1];
            else
            {
                position = next;
                next++;
            }
            itemCells[position] = new Node<T>(value);

            int parentIndex = FindParentPosition(value);
            Node<T> parent = itemCells[parentIndex];
            if (comparison(value, parent.Value) > 0)
                parent.Right = position;
            else
                parent.Left = position;
            itemCells[parentIndex] = parent;
        }

        private int FindParentPosition(T value)
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
