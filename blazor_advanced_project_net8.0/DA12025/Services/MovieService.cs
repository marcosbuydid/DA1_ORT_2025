using DataAccess.Interfaces;
using Domain;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
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
            Movie? movieToDelete = _movieRepository.GetMovie(m => m.Title == title);
            if (movieToDelete == null)
            {
                throw new ArgumentException("Cannot find a movie with this title");
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
            Movie? movie = _movieRepository.GetMovie(m => m.Title == movieToUpdate.Title);
            if (movie == null)
            {
                throw new ArgumentException("Cannot find the specified movie");
            }

            movie.Title = movieToUpdate.Title;
            movie.Director = movieToUpdate.Director;
            movie.ReleaseDate = movieToUpdate.ReleaseDate;
            _movieRepository.UpdateMovie(movie);
        }

        public MovieDTO GetMovie(string title)
        {
            Movie? movie = _movieRepository.GetMovie(movie => movie.Title == title);
            if (movie == null)
            {
                throw new ArgumentException("Cannot find a movie with this title");
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
            return new Movie(movieDTO.Id, movieDTO.Title, movieDTO.Director, movieDTO.ReleaseDate, movieDTO.Budget);
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