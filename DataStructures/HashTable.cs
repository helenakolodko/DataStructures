using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class HashTable<TKey, TValue>: IEnumerable<TValue>
    {
        private class Pair<TKey, TValue>
        {
            internal TKey Key;
            internal TValue Value;
            internal int Hash;
            internal Pair<TKey, TValue> Next;
        }

        private int bucketsCount;
        private Pair<TKey, TValue>[] buckets;
        private EqualityComparer<TKey> comparer;

        public HashTable(EqualityComparer<TKey> comparer):this(128, comparer)
        {
        }

        public HashTable ()
            : this(128, null)
	    {
	    }

        public HashTable(int buckets, EqualityComparer<TKey> comparer)
        {
            bucketsCount = buckets;
            this.buckets = new Pair<TKey, TValue>[buckets];
            if (comparer == null)
                this.comparer = EqualityComparer<TKey>.Default;
            else
                this.comparer = comparer;
        }

        public TValue Get(TKey key)
        {
            int hashCode = comparer.GetHashCode(key);
            int index = FindBucket(hashCode);
            TValue value = default(TValue);
            if (buckets[index] == null || !TryGetValue(buckets[index], key, out value))
                throw new InvalidOperationException("Hash Table doesn't contain value with this key.");
            else
                return value;        
        }
        public void Add(TKey key, TValue value) 
        {
            if (ReferenceEquals(key, null))
                throw new ArgumentNullException("key");
            int hashCode = comparer.GetHashCode(key);
            int index = FindBucket(hashCode);
            if (buckets[index] == null)
                buckets[index] = new Pair<TKey, TValue>() { Key = key, Value = value, Hash = hashCode };
            else
            {
                if (ContainsKey(buckets[index], key))
                    throw new InvalidOperationException("Hash Table already contains value with this key.");
                AddPair(buckets[index], new Pair<TKey, TValue>() { Key = key, Value = value, Hash = hashCode });
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            for (int i = 0; i < bucketsCount; i++)
                if (buckets[i] != null)
                {
                    var pair = buckets[i];
                    while (pair!= null)
                    {
                        yield return pair.Value;
                        pair = pair.Next;
                    }
                }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int FindBucket(int hashCode)
        {
            return (int)((ulong)hashCode % (ulong)bucketsCount);
        }

        private void AddPair(Pair<TKey, TValue> head, Pair<TKey, TValue> pair)
        {
            while (head.Next != null)
                head = head.Next;
            head.Next = pair;
        }

        private bool ContainsKey(Pair<TKey, TValue> head, TKey key)
        {
            while (head != null)
                if (head.Key.Equals(key))
                    return true;
                else
                    head = head.Next;
            return false;
        }

        private bool TryGetValue(Pair<TKey, TValue> head, TKey key, out TValue value)
        {
            value = default(TValue);
            while (head != null)
                if (head.Key.Equals(key))
                {
                    value = head.Value;
                    return true;
                }
                else
                    head = head.Next;
            return false;
        }
    }
}
