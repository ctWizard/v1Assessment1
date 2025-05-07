

using testThurs;


namespace Tests;
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        MyLinkedList<Movie> _movieList = new();
        Movie movie = new Movie{
            MovieID="123456",
            Title="TestMovie",
            Director="Corey",
            Genre="Satire",
            ReleaseYear=2000,
            Availible=true
        };
        _movieList.Add(movie);
        var result = _movieList.FindByTitle("TestMovie");
        Assert.NotNull(result);

       
        
    }
}
