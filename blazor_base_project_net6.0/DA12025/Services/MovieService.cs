using DataAccess;
using Domain;
using Services.Interfaces;

namespace Services
{
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

            _inMemoryDatabase.Movies.Add(movie);
        }

        public void DeleteMovie(string title)
        {
            Movie movieToDelete = GetMovie(title);
            _inMemoryDatabase.Movies.Remove(movieToDelete);
        }

        public List<Movie> GetMovies()
        {
            return _inMemoryDatabase.Movies;
        }

        public void UpdateMovie(Movie movieToUpdate)
        {
            Movie? movie = _inMemoryDatabase.Movies.Find(m => m.Title == movieToUpdate.Title);
            var movieToUpdateIndex = _inMemoryDatabase.Movies.IndexOf(movie);

            _inMemoryDatabase.Movies[movieToUpdateIndex] = movieToUpdate;
        }

        public Movie GetMovie(string title)
        {
            Movie? movie = _inMemoryDatabase.Movies.FirstOrDefault(movie => movie.Title == title);
            if (movie == null)
            {
                throw new ArgumentException("Cannot find movie with this title");
            }
            return movie;
        }

        private void ValidateUniqueTitle(String title)
        {
            foreach (var movie in _inMemoryDatabase.Movies)
            {
                if (movie.Title == title)
                {
                    throw new ArgumentException("There`s a movie already defined with that title");
                }
            }
        }
    }
}
