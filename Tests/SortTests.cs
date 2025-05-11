using testThurs;

namespace Tests;

public class SortTests{
    [Fact]
    public void BubbleSort(){
        //test is sorting by title so movie3 should not be first in linked list after sort
        MyLinkedList<Movie> _movieList = new();
        
        Movie movie = new Movie{
            MovieID="1",Title="TestMovie1",Director="Corey",Genre="Satire",ReleaseYear=2000,Availible=true
        };
        Movie movie2 = new Movie{
            MovieID="2",Title="TestMovie2",Director="Not Corey",Genre="Satire",ReleaseYear=2000,Availible=true
        };
        Movie movie3 = new Movie{
            MovieID="3",Title="TestMovie3",Director="Korey with a K",Genre="Satire",ReleaseYear=2000,Availible=true
        };
        _movieList.Add(movie3);
        _movieList.Add(movie);
        _movieList.Add(movie2);
        _movieList.BubbleSortByTitle();
        Movie firstMovie = _movieList.GetFirst();
        Assert.NotNull(firstMovie);
        Assert.Equal("TestMovie1",firstMovie.Title);

        
        
    }
    [Fact]
    public void MergeSort(){
        MyLinkedList<Movie> _movieList = new();
        //sort movies by release year so movie3 should be first
        Movie movie = new Movie{
            MovieID="1",Title="TestMovie1",Director="Corey",Genre="Satire",ReleaseYear=2003,Availible=true
        };
        Movie movie2 = new Movie{
            MovieID="2",Title="TestMovie2",Director="Not Corey",Genre="Satire",ReleaseYear=2002,Availible=true
        };
        Movie movie3 = new Movie{
            MovieID="3",Title="TestMovie3",Director="Korey with a K",Genre="Satire",ReleaseYear=2001,Availible=true
        };
        
        _movieList.Add(movie);
        _movieList.Add(movie2);
        _movieList.Add(movie3);
        _movieList.MergeSortByReleaseYear();
        Movie? firstMovie = _movieList.GetFirst();
        Assert.NotNull(firstMovie);
        Assert.Equal("TestMovie3",firstMovie.Title);

    }
}