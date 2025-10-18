using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Models
{
    public class MovieSubImage
    {
        public int Id { get; set; }
        [Required] 
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public string Img { get; set; } = string.Empty;
    }

}
