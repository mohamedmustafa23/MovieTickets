using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Models
{
    public enum MovieState
    {
        NowShowing,
        ComingSoon,

    }
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public MovieState Status { get; set; } 

        public DateTime DateTime { get; set; }

        // Main and sub images
        public string? MainImg { get; set; }

        // Foreign keys
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        [Required]
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; } = null!;

        public List<MovieActor> MovieActors { get; set; } = new();
        public List<MovieSubImage> SubImages { get; set; } = new();
    }

}
