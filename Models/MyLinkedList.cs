using System;
using System.Collections.Generic;

namespace testThurs.Models
{
    public class MyLinkedList<T>
    {
        private class Node
        {
            public T Data;
            public Node? Next;
            public Node(T data) => Data = data;
        }

        private Node? head;

        public void Add(T item)
        {
            var newNode = new Node(item);
            if (head == null)
                head = newNode;
            else
            {
                var current = head;
                while (current.Next != null)
                    current = current.Next;
                current.Next = newNode;
            }
        }

        public bool Remove(Predicate<T> match)
        {
            Node? current = head;
            Node? previous = null;

            while (current != null)
            {
                if (match(current.Data))
                {
                    if (previous == null)
                        head = current.Next;
                    else
                        previous.Next = current.Next;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            return false;
        }

        public T? Find(Predicate<T> match)
        {
            var current = head;
            while (current != null)
            {
                if (match(current.Data))
                    return current.Data;
                current = current.Next;
            }
            return default;
        }

        public List<T> ToList()
        {
            var result = new List<T>();
            var current = head;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Next;
            }
            return result;
        }

        public IEnumerable<T> Enumerate()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

    }
}
