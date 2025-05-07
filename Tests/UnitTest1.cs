

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
}
