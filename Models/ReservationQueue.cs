namespace testThurs.Models
{
    public class ReservationQueue
    {
        private class Node
        {
            public string Data;
            public Node? Next;

            public Node(string data)
            {
                Data = data;
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

            string result = _head.Data;
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
            return _head?.Data;
        }
        public bool IsEmpty()
        {
            return _count == 0;
        }
    }
}