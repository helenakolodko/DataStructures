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


    }
}
