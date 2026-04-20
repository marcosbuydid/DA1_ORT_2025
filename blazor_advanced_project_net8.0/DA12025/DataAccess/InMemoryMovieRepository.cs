using Domain;
using Services.Interfaces.Repositories;

namespace DataAccess
{
    public class InMemoryMovieRepository : IMovieRepository
    {
        private List<Movie> Movies { get; }

        public InMemoryMovieRepository()
        {
            Movies = new List<Movie>();
            LoadDefaultMovies();
        }

        private void LoadDefaultMovies()
        {
            Movies.Add(new Movie(null, "Black Rain", "Ridley Scott", new DateTime(1989, 09, 22), 30000000));
            Movies.Add(new Movie(null, "Cast Away", "Robert Zemeckis", new DateTime(2000, 12, 22), 25000000));
            Movies.Add(new Movie(null, "Training Day", "Antoine Fuqua", new DateTime(2002, 01, 18), 10000000));
        }

        public List<Movie> GetMovies()
        {
            return Movies;
        }

        public Movie? GetMovie(Func<Movie, bool> filter)
        {
            return Movies.Where(filter).FirstOrDefault();
        }

        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }

        public void UpdateMovie(Movie movieToUpdate)
        {
            Movie? movie = Movies.Find(m => m.Title == movieToUpdate.Title);
            var movieToUpdateIndex = Movies.IndexOf(movie);
            movieToUpdate.Budget = movie.Budget;
            Movies[movieToUpdateIndex] = movieToUpdate;
        }

        public void DeleteMovie(Movie movie)
        {
            Movies.Remove(movie);
        }
    }
}