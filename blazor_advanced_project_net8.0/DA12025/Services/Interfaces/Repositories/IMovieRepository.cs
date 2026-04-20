using Domain;

namespace Services.Interfaces.Repositories;

public interface IMovieRepository
{
    List<Movie> GetMovies();
    Movie? GetMovie(Func<Movie, bool> filter);
    void AddMovie(Movie movie);
    void UpdateMovie(Movie movie);
    void DeleteMovie(Movie movie);
}