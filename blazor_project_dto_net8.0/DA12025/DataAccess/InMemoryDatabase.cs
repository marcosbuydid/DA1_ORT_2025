using Domain;

namespace DataAccess
{
    public class InMemoryDatabase
    {
        private List<Movie> Movies { get; }

        public InMemoryDatabase()
        {
            Movies = new List<Movie>();
            LoadDefaultMovies();
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

        private void LoadDefaultMovies()
        {
            Movies.Add(new Movie("Black Rain", "Ridley Scott", new DateTime(1989, 09, 22), 30000000));
            Movies.Add(new Movie("Cast Away", "Robert Zemeckis", new DateTime(2000, 12, 22), 25000000));
            Movies.Add(new Movie("Training Day", "Antoine Fuqua", new DateTime(2002, 01, 18), 10000000));
        }
    }
}