using MovieTickets.Models;

namespace MovieTickets.ViewModels
{
    public class MovieVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<CinemaHall> CinemaHalls { get; set; }
        public IEnumerable<Actor> Actors { get; set; }        
        public Movie? Movie { get; set; }
    }
}
