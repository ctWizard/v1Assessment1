using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using testThurs.Models;


namespace MovieLibraryApp
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

        //Creates linked list that holds all raw data
        private MyLinkedList<Movie> _movieList = new();

        private MyHashTable _movieHashTable = new MyHashTable();



        
        //movies is the left listbox
        public ObservableCollection<Movie> MoviesLeft { get; set; } = new ObservableCollection<Movie>();

        private Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                _selectedMovie = value;
                OnPropertyChanged();
            }
        }
        


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
            

            MoviesLeft.Clear();

            foreach (var movie in _movieList.GetAllMovies())
            {
                MoviesLeft.Add(movie);
            }
            

        }
    }
}
