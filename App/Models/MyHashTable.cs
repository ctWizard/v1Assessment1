
namespace testThurs
{
    public class MyHashTable
    {
        private class HashNode
        {
            public string Key { get; }
            public Movie Value { get; }
            public HashNode? Next { get; set; }


            public HashNode(string key, Movie value)
            {
                Key = key;
                Value = value;
            }
        }
        

        private readonly int _capacity;
        private readonly HashNode?[] _buckets;

        public MyHashTable(int capacity = 100)
        {
            _capacity = capacity;
            _buckets = new HashNode[_capacity];
        }

        private int GetIndex(string key)
        {
            int hashCode = key.GetHashCode();
            int index = Math.Abs(hashCode) % _capacity;
            return index;
        }

        public bool Add(string key, Movie value)
        {
            int index = GetIndex(key);
            HashNode? head = _buckets[index];

            while (head != null)
            {
                if (head.Key == key)
                {
                    return false;
                }
                head = head.Next;

            }

            var newNode = new HashNode(key, value)
            {
                Next = _buckets[index]
            };
            _buckets[index] = newNode;
            return true;

        }

        public Movie? Get(string key)
        {
            int index = GetIndex(key.Trim());
            HashNode? head = _buckets[index];

            while (head != null)
            {
                if (head.Key == key)
                {
                    return head.Value;
                }
                head = head.Next;
            }
            return null; //no match
        }
    }
}