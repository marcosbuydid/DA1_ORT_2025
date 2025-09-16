using Domain;

namespace DataAccess;

public class InMemoryDatabase
{
    private List<Movie> Movies { get; }

    public InMemoryDatabase()
    {
        Movies = new List<Movie>();
    }

    public List<Movie> GetMovies()
    {
        return Movies;
    }

    public Movie? GetMovie(string title)
    {
        Movie? movie = Movies.FirstOrDefault(movie => movie.Title == title);
        return movie;
    }

    public void AddMovie(Movie movie)
    {
        Movies.Add(movie);
    }

    public void DeleteMovie(Movie movie)
    {
        Movies.Remove(movie);
    }

    public void UpdateMovie(Movie movieToUpdate)
    {
        Movie? movie = Movies.Find(m => m.Title == movieToUpdate.Title);
        var movieToUpdateIndex = Movies.IndexOf(movie);
        Movies[movieToUpdateIndex] = movieToUpdate;
    }
}