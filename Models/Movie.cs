using testThurs.Models;
using testThurs.Services;

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

        public ReservationQueue ReservationQueue { get; } = new ReservationQueue();

        public void AddReservation(string resurvedName)
        {
            ReservationQueue.Enqueue(resurvedName);
        }

        public string? ServeNextPerson()
        {
            return ReservationQueue.Dequeue();
        }
        
    }
}
