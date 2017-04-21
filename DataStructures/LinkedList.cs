using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private struct Node<T>
        {
            internal T Value;
            internal int Next;
            internal int Previous;
            public Node(T value, int previous)
            {
                Value = value;
                Next = -1;
                Previous = previous;
            }
        }

        private int head;
        private int tail;
        private Node<T>[] itemCells;
        private int count;
        private int next;
        private int[] freeCells;
        private int freeCount;
        private int capacity;

        public LinkedList() :
            this(16)
        {
        }

        public LinkedList(T[] items)
            : this(items.Length)
        {
            for (int i = 0; i < capacity; i++)
                Add(items[i]);
        }

        public LinkedList(int capacity)
        {
            this.capacity = capacity;
            itemCells = new Node<T>[capacity];
        }

        public int Count { get { return count; } }

        public void Add(T value)
        {
            if (count == 0)
            {
                itemCells[0] = new Node<T>(value, -1);
                tail = 0;
            }
            else
            {
                if (count >= capacity)
                    ExpandItemsArray();
                itemCells[next] = new Node<T>(value, tail);
                itemCells[tail].Next = next;
                tail = next;
            }
            next++;
            count++;
        }

        public void Remove(T value)
        {
            int found = FindItemPosition(value);
            if (found >= 0)
            {
                Remove(found);
            }
            else
                throw new InvalidOperationException("List doesn't contain this value.");
        }
        
        public bool Contains(T value)
        {
            return FindItemPosition(value) >= 0;
        }
        public void Insert(T item, int position)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            if (count > 0)
            {
                var current = head;
                do
                {
                    var next = itemCells[current].Next;
                    Remove(current);
                    current = next;
                }
                while (current >= 0);
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int FindItemPosition(T value)
        {
            int current = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            if (count > 0)
            {
                do
                {
                    if (comparer.Equals(value, itemCells[current].Value))
                    {
                        return current;
                    }
                    current = itemCells[current].Next;
                }
                while (current >= 0);
            }
            return -1;
        }
        
        private void AddToFreeCells(int found)
        {
            if (freeCells == null)
                freeCells = new int[16];
            else if (freeCells.Length <= freeCount)
            {
                int[] temp = new int[freeCount * 2];
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
        
        private void Remove(int found)
        {
            if (itemCells[found].Previous >= 0)
                itemCells[itemCells[found].Previous].Next = itemCells[found].Next;
            if (itemCells[found].Next >= 0)
                itemCells[itemCells[found].Next].Previous = itemCells[found].Previous;
            count--;
            AddToFreeCells(found);
        }

    }
}
