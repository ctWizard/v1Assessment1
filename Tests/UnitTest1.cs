

using testThurs;


namespace Tests;
public class UnitTest1
{
    [Fact]
    public void AddMovie()
    {
        MyLinkedList<Movie> _movieList = new();
        MyHashTable _movieHashTable = new MyHashTable();
        Movie movie = new Movie{
            MovieID="123456",
            Title="TestMovie",
            Director="Corey",
            Genre="Satire",
            ReleaseYear=2000,
            Availible=true
        };
        _movieList.Add(movie);
        _movieHashTable.Add("idTest",movie);
        var result = _movieList.FindByTitle("TestMovie");
        var result2 = _movieHashTable.Get("idTest");
        Assert.NotNull(result);
        Assert.NotNull(result2);
        
    }

    [Fact]
    public void FindByID(){
        MyHashTable _movieHashTable = new MyHashTable();
        Movie movie = new Movie{
            MovieID="1",Title="TestMovie1",Director="Corey",Genre="Satire",ReleaseYear=2000,Availible=true
        };
        Movie movie2 = new Movie{
            MovieID="2",Title="TestMovie2",Director="Corey",Genre="Satire",ReleaseYear=2000,Availible=true
        };
        Movie movie3 = new Movie{
            MovieID="3",Title="TestMovie3",Director="Corey",Genre="Satire",ReleaseYear=2000,Availible=true
        };
        _movieHashTable.Add("1",movie);
        _movieHashTable.Add("2",movie2);
        _movieHashTable.Add("3",movie3);

        String? result = _movieHashTable.Get("2").Title;
        Assert.NotNull(result);
        Assert.Equal("TestMovie2",result);
    }
    [Fact]
    public void FindByTitle(){
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
        _movieList.Add(movie);
        _movieList.Add(movie2);
        _movieList.Add(movie3);
        String? result= _movieList.FindByTitle("TestMovie3").Director;
        Assert.Equal("Korey with a K",result);
    }
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

    [Fact]
    public void BorrowAndReturnMovie(){
        //Adds item to queue checks its there then removes it and checks again
        MyLinkedList<Movie> _movieList = new();
        Movie movie = new Movie{
            MovieID="1",Title="TestMovie1",Director="Corey",Genre="Satire",ReleaseYear=2003,Availible=true
        };
        _movieList.Add(movie);
        Movie? testCase = _movieList.GetFirst();
        Assert.NotNull(testCase);
        testCase.AddReservation("Corey");
        Assert.Equal("Corey",testCase.ReservationQueue.Peek());
        testCase.ServeNextPerson();
        Assert.True(testCase.ReservationQueue.IsEmpty());
    }

    [Fact]
    public void MultiBorrowAndReturnMovie(){
        MyLinkedList<Movie> _movieList = new();
        Movie movie = new Movie{
            MovieID="1",Title="TestMovie1",Director="Corey",Genre="Satire",ReleaseYear=2003,Availible=true
        };
        _movieList.Add(movie);
        Movie? testCase = _movieList.GetFirst();
        Assert.NotNull(testCase);
        testCase.AddReservation("Corey");
        testCase.AddReservation("Corey2");
        testCase.AddReservation("Corey3");
        
        testCase.ServeNextPerson();
        testCase.ServeNextPerson();
        Assert.Equal("Corey3",testCase.ReservationQueue.Peek());
        //Assert.True(testCase.ReservationQueue.IsEmpty());
    }

    [Fact]
    public void DuplicateMovieId(){
        MyLinkedList<Movie> _movieList = new();
        Movie movie = new Movie{
            MovieID="1",Title="TestMovie1",Director="Corey",Genre="Satire",ReleaseYear=2003,Availible=true
        };
        Movie movie1 = new Movie{
            MovieID="1",Title="TestMovie2",Director="Corey",Genre="Satire",ReleaseYear=2003,Availible=true
        };
        _movieList.Add(movie);
        _movieList.Add(movie1);

        Assert.Equal("TestMovie1",_movieList.FindByID("1").Title);

    }
}
