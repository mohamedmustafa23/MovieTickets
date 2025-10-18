using MovieTickets.Models;

namespace MovieTickets.ViewModels
{
    public class AdminHomeVM
    {
        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
        public IEnumerable<Actor> Actors { get; set; } = new List<Actor>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<CinemaHall> CinemaHalls { get; set; } = new List<CinemaHall>();
    }
}
