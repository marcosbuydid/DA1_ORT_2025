using DataAccess;
using Domain;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly InMemoryMovieRepository _movieRepository;

        public MovieService(InMemoryMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void AddMovie(MovieDTO movie)
        {
            ValidateUniqueTitle(movie.Title);
            _movieRepository.AddMovie(ToEntity(movie));
        }

        public void DeleteMovie(string title)
        {
            Movie? movieToDelete = _movieRepository.GetMovie(title);
            if (movieToDelete == null)
            {
                throw new ArgumentException("Cannot find the specified movie");
            }

            _movieRepository.DeleteMovie(movieToDelete);
        }

        public List<MovieDTO> GetMovies()
        {
            List<MovieDTO> moviesDTO = new List<MovieDTO>();

            foreach (var movie in _movieRepository.GetMovies())
            {
                moviesDTO.Add(FromEntity(movie));
            }

            return moviesDTO;
        }

        public void UpdateMovie(MovieDTO movieToUpdate)
        {
            Movie? movie = _movieRepository.GetMovie(movieToUpdate.Title);
            if (movie == null)
            {
                throw new ArgumentException("Cannot find the specified movie");
            }

            movie.Director = movieToUpdate.Director;
            movie.ReleaseDate = movieToUpdate.ReleaseDate;
            //in the example budget is non-updatable
            //this line is added to avoid budget value 
            //be on cero after update
            movieToUpdate.Budget = movie.Budget;
            _movieRepository.UpdateMovie(movie);
        }

        public MovieDTO GetMovie(string title)
        {
            Movie? movie = _movieRepository.GetMovie(title);
            if (movie == null)
            {
                throw new ArgumentException("Cannot find movie with this title");
            }

            return FromEntity(movie);
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

        private static Movie ToEntity(MovieDTO movieDTO)
        {
            return new Movie(movieDTO.Title, movieDTO.Director, movieDTO.ReleaseDate, movieDTO.Budget);
        }

        private static MovieDTO FromEntity(Movie movie)
        {
            return new MovieDTO()
            {
                Title = movie.Title,
                Director = movie.Director,
                ReleaseDate = movie.ReleaseDate
            };
        }
    }
}