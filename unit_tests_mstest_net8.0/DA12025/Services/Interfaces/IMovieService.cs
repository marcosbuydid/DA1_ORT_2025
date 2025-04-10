using Domain;

namespace Services.Interfaces;

public interface IMovieService
{
    List<Movie> GetMovies();
    void AddMovie(Movie movie);
    void DeleteMovie(string title);
    void UpdateMovie(Movie movie);
    Movie GetMovie(string title);
}