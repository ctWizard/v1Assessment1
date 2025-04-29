using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;


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
                if (match == (current.Data))
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

        public Movie? FindByTitle(string title)
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


        public void BubbleSortByTitle()
        {
            if (head == null || head.Next == null)
            {
                return; // linked list is to small to sort as it is less then 2 entrys long
            }

            bool swapped;

            do
            {
                swapped = false;
                Node? current = head;

                while (current != null && current.Next !=null)
                {
                    Movie movie1 = current.Data;
                    Movie movie2 = current.Next.Data;

                    if (string.Compare(movie1.Title, movie2.Title, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        var temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;

                        swapped = true;
                    }
                    current = current?.Next;
                    
                }
            }
            while (swapped);


        }

        //merge sort function
        public void MergeSortByTitle()
        {
            head = MergeSort(head);
        }
        private Node? MergeSort(Node? node)
        {
            if (head==null && head.Next == null) 
                return null;
            Node middle = GetMiddle(head);
            Node nextOfMiddle = middle.Next;

            middle.Next = null;

            Node left = MergeSort(head);
            Node right = MergeSort(nextOfMiddle);

            Node sortedList = SortedMerge(left, right);
            return sortedList;
        }

        private Node SortedMerge(Node? a,Node? b)
        {
            if (a == null)
                return b!;
            if (b == null)
                return a;

            Node result;

            if (string.Compare(a.Data.Title,b.Data.Title, StringComparison.OrdinalIgnoreCase)<=0)
            {
                result = a;
                result.Next = SortedMerge(a.Next, b);
            }
            else
            {
                result = b;
                result.Next = SortedMerge(a, b.Next);
            }
            return result;
        }

        private Node GetMiddle(Node head)
        {
            if (head == null)
                return head;

            Node slow = head;
            Node? fast = head.Next;

            while (fast != null)
            {
                slow = fast; fast = fast.Next;
            }

            return slow;

        }
        // end of merge sort code
        public List<Movie> OutputList()
        {
            Node? current = head;
            List<Movie> output = new List<Movie>();
            while (current != null)
            {
                output.Add(current.Data);
                current = current.Next;
            }
            return output;
        }

        //export code
        public void ExportToCsv(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("MovieID,Title,Director,Genre,ReleaseYear,Available,queueData");

                Node? current = head;
                while (current != null)
                {
                    Movie movie = current.Data;
                    //sanitise data
                    string safeTitle = movie.Title.Replace(",", " ");
                    string safeDirector = movie.Director.Replace(",", " ");
                    string safeGenre = movie.Genre.Replace(",", " ");

                    //check if queue exists then act accordingly
                    string queueData = string.Empty;
                    if (movie.ReservationQueue != null && !movie.ReservationQueue.IsEmpty())
                    {
                        queueData = string.Join("|",movie.ReservationQueue.ToList());
                    }


                    writer.WriteLine($"{movie.MovieID},{safeTitle},{safeDirector},{safeGenre},{movie.ReleaseYear},{movie.Availible},{queueData}");

                    current = current.Next;
                }
                }
            }
    }
}
