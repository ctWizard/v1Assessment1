using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using testThurs.Models;
using testThurs.Services;

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

        private readonly IExportService _exportService;

        [ObservableProperty]
        //movies is the left listbox
        private ObservableCollection<Movie> movies = new();
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

        

        public MainViewModel() : this(new ExportService()) { }

        public MainViewModel(IExportService exportService)
        {
            _exportService = exportService;
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

                _movieList.Add(movie);
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
        private void Export()
        {
            string path = "books_export.csv";
            _exportService.ExportBooks(_movieList.ToList(), path);
        }

        private void RefreshMovies()
        {
            Movies = new ObservableCollection<Movie>(_movieList.ToList());
        }
    }
}
