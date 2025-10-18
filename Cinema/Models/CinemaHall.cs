using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Models
{
    public class CinemaHall
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Img { get; set; } = "defaultImg.png";

    }
}
