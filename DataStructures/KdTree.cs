using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class KdTree<T>: IEnumerable<T>
    {
        private struct Node<T>
        {
            internal T[] Values;
            internal int Left;
            internal int Right;
            internal int Parent;
            public Node(T[] values, int parent)
            {
                Values = values;
                Left = -1;
                Right = -1;
                Parent = parent;
            }
        }

        private Node<T>[] nodes;
        private int dimensions;

        public KdTree(int dimensions)
        {
            if (dimensions < 1)
                throw new ArgumentOutOfRangeException(nameof(dimensions), "Must be greater or equals to 1");
            this.dimensions = dimensions;

            nodes = new Node<T>[10];
        }

        public void Add(T[] point)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T[] point)
        {
            throw new NotImplementedException();
        }

        public T[] GetNearestNeighbour(T[] point)
        {
            throw new NotImplementedException();
        }

        public void Remove(T[] point)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
