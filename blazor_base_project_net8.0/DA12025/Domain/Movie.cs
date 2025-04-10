namespace Domain;

public class Movie
{
    public string Title { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
 
    public Movie(string title, string director, DateTime releaseYear)
    {
        Title = title;
        Director = director;
        ReleaseDate = releaseYear;
    }
}