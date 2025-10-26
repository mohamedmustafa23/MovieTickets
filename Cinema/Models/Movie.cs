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

        [Required]
        public decimal Price { get; set; }
        [Required]
        public MovieState Status { get; set; } 

        public DateTime DateTime { get; set; }
        [Required]
        public string MainImg { get; set; } = string.Empty;

        // Foreign keys
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        [Required]
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; } = null!;

        [Required]
        public List<MovieActor> MovieActors { get; set; } = new();
        public List<MovieSubImage> SubImages { get; set; } = new();
    }

}
