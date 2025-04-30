namespace testThurs.Models
{
    public class ReservationQueue
    {
        private class Node
        {
            public string Name;
            public Node? Next;

            public Node(string name)
            {
                Name=name;
            }
        }
        private Node? _head;
        private Node? _tail;
        private int _count;

        public int count;

        public int Count => _count;

        public void Enqueue(string item)
        {
            Node newNode = new Node(item);

            if (_tail == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }
        }
        public string? Dequeue()
        {
            if (_head == null)
            {
                return null; //empty queue
            }

            string result = _head.Name;
            _head = _head.Next;

            if (_head == null)
            {
                _tail = null; //queue has been made empty
            }

            _count--;
            return result;
        }

        public string? Peek()
        {
            return _head?.Name;
        }
        public bool IsEmpty()
        {
            return _count == 0;
        }
        public List<string> ToList()
        {
            List<string> list = new List<string>();
            Node? current = _head;
            while (current != null)
            {
                list.Add(current.Name);
                current = current.Next;
            }
            return list;
        }
        public override string ToString()
        {
            var names = new List<string>();
            var current = _head;
            while (current != null)
            {
                names.Add(current.Name);
                current = current.Next;
            }

            return string.Join("|", names);

        }
    }
}