namespace testThurs.Models
{
    public class Movie
    {
        public required string MovieID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Genre {  get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public bool Availible { get; set; }
    }
}
