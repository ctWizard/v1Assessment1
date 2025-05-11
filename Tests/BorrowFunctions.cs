using testThurs;
namespace Tests;

public class BorrowFunctions{
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

}