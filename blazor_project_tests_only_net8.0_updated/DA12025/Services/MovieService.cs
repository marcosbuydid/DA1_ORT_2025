using DataAccess;
using Domain;
using Services.Interfaces;

namespace Services;

public class MovieService : IMovieService
{
    private readonly InMemoryMovieRepository _movieRepository;

    public MovieService(InMemoryMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public void AddMovie(Movie movie)
    {
        ValidateUniqueTitle(movie.Title);
        _movieRepository.AddMovie(movie);
    }

    public void DeleteMovie(string title)
    {
        Movie movieToDelete = GetMovie(title);
        _movieRepository.DeleteMovie(movieToDelete);
    }

    public List<Movie> GetMovies()
    {
        return _movieRepository.GetMovies();
    }

    public void UpdateMovie(Movie movieToUpdate)
    {
        Movie? movie = _movieRepository.GetMovie(movieToUpdate.Title);
        if (movie == null)
        {
            throw new ArgumentException("Cannot find the specified movie");
        }

        movie.Title = movieToUpdate.Title;
        movie.Director = movieToUpdate.Director;
        movie.ReleaseDate = movieToUpdate.ReleaseDate;
        _movieRepository.UpdateMovie(movie);
    }

    public Movie GetMovie(string title)
    {
        Movie? movie = _movieRepository.GetMovie(title);
        if (movie == null)
        {
            throw new ArgumentException("Cannot find movie with this title");
        }

        return movie;
    }

    private void ValidateUniqueTitle(string title)
    {
        string inputTitle = title.Trim().ToLowerInvariant();
        foreach (var movie in _movieRepository.GetMovies())
        {
            string retrievedTitle = movie.Title.Trim().ToLowerInvariant();
            if (retrievedTitle == inputTitle)
            {
                throw new ArgumentException("There`s a movie already defined with that title");
            }
        }
    }
}