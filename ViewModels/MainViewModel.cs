using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using testThurs.Models;
using testThurs.Services;

namespace MovieLibraryApp
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string newId = string.Empty;

        [ObservableProperty]
        private string newTitle = string.Empty;

        [ObservableProperty]
        private string newDirector = string.Empty;

        [ObservableProperty]
        private string newYear = string.Empty;

        private MyLinkedList<Movie> _movieList = new();
        private readonly IExportService _exportService;

        [ObservableProperty]
        private ObservableCollection<Movie> movies = new();

        [ObservableProperty]
        private string searchResult = string.Empty;

        public MainViewModel() : this(new ExportService()) { }

        public MainViewModel(IExportService exportService)
        {
            _exportService = exportService;
            RefreshMovies();
        }

        [RelayCommand]
        private void AddBook()
        {
            if (int.TryParse(newYear, out int parsedYear))
            {
                var movie = new Movie
                {
                    MovieID = newId,
                    Title = newTitle,
                    Director = newDirector,
                    ReleaseYear = parsedYear,
                    Availible = true,
                };

                _movieList.Add(movie);
                RefreshMovies();

                
            }
            else
            {
                SearchResult = "Invalid year.";
            }
        }


        [RelayCommand]
        private void SearchBook(string title)
        {
            var result = _movieList.Find(b => b.Title.ToLower() == title.ToLower());
            SearchResult = result != null ? $"Found: {result.Title} by {result.Director}" : "Movie not found.";
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
