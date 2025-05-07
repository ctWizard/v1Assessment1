
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;



namespace testThurs
{
    public partial class MainViewModel : ObservableObject
    {
        //propertys for the new data inputs
        [ObservableProperty]
        private string newId = string.Empty;
        [ObservableProperty]
        private string newTitle = string.Empty;
        [ObservableProperty]
        private string newDirector = string.Empty;
        [ObservableProperty]
        private string newGenre = string.Empty;
        [ObservableProperty]
        private string newYear = string.Empty;
        [ObservableProperty]
        private bool newAval = true;

        //Property for selecting movie to borrow by id
        [ObservableProperty]
        private string movieIdToBorrow = string.Empty;

        [ObservableProperty]
        private string borrowerName = string.Empty;

        [ObservableProperty]
        private string returnId = string.Empty;

        //Creates linked list that holds all raw data
        public MyLinkedList<Movie> _movieList = new();

        public MyHashTable _movieHashTable = new MyHashTable();



        
        
        


        //search results is the right datagrid
        private ObservableCollection<Movie> _searchResults = new();

        public ObservableCollection<Movie> SearchResults { 
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }

        }
        //search title is the string for finding movies by title
        private ObservableCollection<Movie> searchTitle = new();

        


        [RelayCommand]
        private void Borrow_Click()
        {
            Movie? movie = _movieHashTable.Get(MovieIdToBorrow);
            if (movie == null)
            {
                MessageBox.Show($"{movieIdToBorrow} does not match a movie");
                return;
            }

            if (!movie.Availible)
            {
                if (borrowerName.Length > 1)
                {
                    MessageBox.Show($"{borrowerName} has been added to the queue for{movie.Title}");
                    movie.ReservationQueue.Enqueue(borrowerName);
                    RefreshMovies();
                    return;
                }
                MessageBox.Show($"Name to short");

            }
            else
            {
                MessageBox.Show($"{borrowerName} has borrowed {movie.Title}");
                movie.Availible = false;
                movie.ReservationQueue.Enqueue(borrowerName);
                RefreshMovies();
                return;
            }
        }

        [RelayCommand]
        private void ReturnMovie()
        {
            if (string.IsNullOrWhiteSpace(returnId))
            {
                MessageBox.Show("Please enter a valid movie ID.");
                return;
            }
            Movie? movie = _movieHashTable.Get(returnId);

            if (movie == null)
            {
                MessageBox.Show($"No movie found with ID {returnId}");
                return;
            }   
                
            if (!movie.Availible)
            {
                if (!movie.ReservationQueue.IsEmpty())
                {
                    string returningUser = movie.ReservationQueue.Dequeue();
                    if (!movie.ReservationQueue.IsEmpty())
                    {
                        string nextUser = movie.ReservationQueue.Peek();
                        MessageBox.Show($"{ returningUser} has returned {movie.Title} movie has been given to {movie.ReservationQueue.Peek()}");
                    }
                    else
                    {
                        movie.Availible = true;
                        MessageBox.Show($"{returningUser} has returned movie {movie.Title} noone else in queue");
                    }
                }
                else
                { //Edge case should not trigger
                    movie.Availible = true;

                }

            }
            else
            {
                MessageBox.Show("This movie has already been marked as availible.");
            }
                RefreshMovies();



        }
        


        [RelayCommand]
        private void AddMovie()
        {
            if (int.TryParse(NewYear, out int parsedYear))
            {
                var movie = new Movie
                {
                    MovieID = NewId,
                    Title = NewTitle,
                    Director = NewDirector,
                    Genre = NewGenre,
                    ReleaseYear = parsedYear,
                    Availible = true,
                };

                

                bool check = _movieHashTable.Add(NewId, movie);
                if (check==false) { MessageBox.Show("Duplicate ID value '" + NewId + "' is already in use");}
                else _movieList.Add(movie);
                RefreshMovies();
                NewId = ""; 
                NewTitle = "";
                NewDirector = "";
                NewGenre = "";
                NewYear = "";

            }
            
        }

        [RelayCommand]
        private void SearchTitleCmd(string title)
        {
            SearchResults.Clear();
            var movie = _movieList.FindByTitle(title);
            if (movie != null) { SearchResults.Add(movie); }
            
        }
        [RelayCommand]
        private void SearchIdCmd(string id)
        {  
            SearchResults.Clear();
            Movie? movie = _movieHashTable.Get(id);
            if (movie != null) { SearchResults.Add(movie); }
        }


        [RelayCommand]
        public void ImportMoviesFromFile()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv"
            };
            if (dialog.ShowDialog() == true)
            {
                _movieList.ImportFromCsv(dialog.FileName);
                List<Movie> movieListTemp = new List<Movie>();

                foreach (var movie in movieListTemp)
                {
                    _movieHashTable.Add(NewId, movie);
                }
                    RefreshMovies() ;
                
                

            }

            
        }




        [RelayCommand]
        private void SortBubble()
        {
            SearchResults.Clear();
            _movieList.BubbleSortByTitle();
            List<Movie> output = _movieList.ToList();
            foreach (Movie movie in output)
            {
                SearchResults.Add(movie);
            }

        }

        [RelayCommand]
        private void SortMerge()
        {
            SearchResults.Clear();
            _movieList.MergeSortByTitle();
            List<Movie> output = _movieList.ToList();
            foreach (Movie movie in output)
            {
                SearchResults.Add(movie);
            }
            RefreshMovies();
        }
        [RelayCommand]
        private void Export()
        {
            //"C:\Users\corey\.gitconfig"
            string path = "C:\\Users/corey/movies.csv";
            _movieList.ExportToCsv(path);
        }

        private void RefreshMovies()
        {
            

            SearchResults.Clear();

           
            List<Movie> output = _movieList.ToList();
            foreach (Movie movie in output)
            {
                SearchResults.Add(movie);
            }
            


        }

       





    }
}
