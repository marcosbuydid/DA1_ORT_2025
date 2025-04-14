using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class MovieDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Director is required.")]
        public string Director { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Budget is required.")]
        public int Budget { get; set; }
    }
}