using Domain;

namespace DataAccess.Interfaces;

public interface IMovieRepository
{
    List<Movie> GetAllMovies();
    Movie? Get(Func<Movie, bool> filter);
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(Movie movie);
}