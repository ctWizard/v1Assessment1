

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

    }
    [Fact]
    public void MergeSort(){
        
    }
}
