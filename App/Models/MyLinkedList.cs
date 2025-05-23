using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;


namespace testThurs
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
        //link to hash table
        private MyHashTable _hashTable = new MyHashTable();

        public IEnumerable<Movie> GetAllMovies()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        public Movie? GetFirst(){
            if (head !=null)
                return head.Data;
            else return null;
        }

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
            Movie movie = newNode.Data;
            _hashTable.Add(movie.MovieID, movie);
        }

        public void ImportFromCsv(string filePath)
        {
            if (!File.Exists(filePath)) return;

            using var reader = new StreamReader(filePath);

            string? line;
            bool isFirstLine = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }
                var parts = line.Split(',');

                if (parts.Length >= 6)
                {
                    var movie = new Movie
                    {
                        MovieID = parts[0],
                        Title = parts[1],
                        Director = parts[2],
                        Genre = parts[3],
                        ReleaseYear = int.TryParse(parts[4], out var releaseYear) ? releaseYear : 0,
                        Availible = bool.TryParse(parts[5], out var available) && available
                    };
                    Add(movie);
                }
                
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
        public Movie? FindByID(string id)
        {
            Node? current = head;
            while (current != null)
            {
                if (current.Data.MovieID == id)
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

        public List<string> ToStringList()
        {
            var result = new List<string>();
            var current = head;
            while (current != null)
            {
                Movie movie = current.Data;
                result.Add($"{movie.MovieID} {movie.Title}");
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
                Node current = head;

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
                    current = current.Next;
                    
                }
            }
            while (swapped);


        }

        //merge sort function
        public void MergeSortByReleaseYear()
        {
            head = MergeSort(head);
        }
        private Node? MergeSort(Node? node)
        {
            if (node==null || node.Next == null) 
                return node;
            Node? middle = GetMiddle(node);
            if (middle == null || middle == node)
                return node;
            Node nextOfMiddle = middle.Next;
            middle.Next = null;

            Node left = MergeSort(node);
            Node right = MergeSort(nextOfMiddle);

            Node sortedList = SortedMerge(left, right);
            return sortedList;
        }

        private Node SortedMerge(Node? a,Node? b)
        {
            if (a == null)
                return b;
            if (b == null)
                return a;

            Node result;
            if (a.Data.ReleaseYear <= b.Data.ReleaseYear){
                result = a;
                result.Next = SortedMerge(a.Next,b);
            }
            else
            {
                result = b;
                result.Next = SortedMerge(a, b.Next);
            }
            return result;
        }

        private Node? GetMiddle(Node head)
        {
            if (head == null)
                return null;

            Node slow = head;
            Node? fast = head.Next;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next; 
                fast = fast.Next.Next;
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
