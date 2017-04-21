using System;

namespace DataStructures
{
    public class LinkedList<T>
    {
        private struct Node<T>
        {
            internal T Value;
            internal int Next;
            public Node(T value)
            {
                Value = value;
                Next = -1;
            }
        }

        private int head;
        private Node<T>[] itemCells;
        private int count;
        private int next;
        private int[] freeCells;
        private int freeCount;
        private int capacity;

        public int Count { get { return count; } }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(T item, int position)
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }
        public void Contatins()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
