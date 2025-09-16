using DataAccess;
using Domain;
using Services.Interfaces;

namespace Services;

public class MovieService : IMovieService
{
    private readonly InMemoryDatabase _inMemoryDatabase;

    public MovieService(InMemoryDatabase inMemoryDatabase)
    {
        _inMemoryDatabase = inMemoryDatabase;
    }

    public void AddMovie(Movie movie)
    {
        ValidateUniqueTitle(movie.Title);
        _inMemoryDatabase.AddMovie(movie);
    }

    public void DeleteMovie(string title)
    {
        Movie movieToDelete = GetMovie(title);
        _inMemoryDatabase.DeleteMovie(movieToDelete);
    }

    public List<Movie> GetMovies()
    {
        return _inMemoryDatabase.GetMovies();
    }

    public void UpdateMovie(Movie movieToUpdate)
    {
        Movie? movie = _inMemoryDatabase.GetMovie(movieToUpdate.Title);
        if (movie == null)
        {
            throw new ArgumentException("Cannot find the specified movie");
        }

        movie.Title = movieToUpdate.Title;
        movie.Director = movieToUpdate.Director;
        movie.ReleaseDate = movieToUpdate.ReleaseDate;
        _inMemoryDatabase.UpdateMovie(movie);
    }

    public Movie GetMovie(string title)
    {
        Movie? movie = _inMemoryDatabase.GetMovie(title);
        if (movie == null)
        {
            throw new ArgumentException("Cannot find movie with this title");
        }

        return movie;
    }

    private void ValidateUniqueTitle(string title)
    {
        foreach (var movie in _inMemoryDatabase.GetMovies())
        {
            if (movie.Title == title)
            {
                throw new ArgumentException("There`s a movie already defined with that title");
            }
        }
    }
}