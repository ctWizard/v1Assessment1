using System;
using System.Collections.Generic;

namespace testThurs.Models
{
    public class MyLinkedList<T>
    {
        private class Node
        {
            public Movie Data;
            public Node? Next;
            public Node(Movie data) => Data = data;
        }

        private Node? head;

        public void Add(Movie item)
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

        public bool Remove(Movie match)
        {
            Node? current = head;
            Node? previous = null;

            while (current != null)
            {
                if (match==(current.Data))
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

        public Movie FindByTitle(string title)
        {
            Node? current = head;
            while (current != null)
            {
                if (current.Data.Title == title)
                { return current.Data; }
                current = current.Next;
            }
            return null;
        }

        public List<Movie> ToList()
        {
            var result = new List<Movie>();
            var current = head;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Next;
            }
            return result;
        }


    }
}
