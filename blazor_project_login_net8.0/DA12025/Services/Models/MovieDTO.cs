using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class MovieDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Director is required.")]
        public string Director { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Budget is required.")]
        public int Budget { get; set; }

        public MovieDTO() {}

        public MovieDTO(string title, string director, DateTime releaseDate, int budget)
        {
            Title = title;
            Director = director;
            ReleaseDate = releaseDate;
            Budget = budget;
        }
    }
}