using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class MovieServiceTests
{
    private InMemoryDatabase _inMemoryDatabase;
    private MovieService _movieService;
    private Movie _movie;
    private MovieDTO _movieDTO;

    [TestInitialize]
    public void Setup()
    {
        _inMemoryDatabase = new InMemoryDatabase();
        _movieService = new MovieService(_inMemoryDatabase);
        _movie = new Movie("Sing Sing", "Greg Kwedar", new DateTime(2024, 07, 12), 2000000);
        _movieDTO = new MovieDTO("Sing Sing", "Greg Kwedar", DateTime.Today, 2000000);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddMovie_WhenAddAMovieTwice_ThenThrowException()
    {
        //arrange
        _inMemoryDatabase.GetMovies().Clear();
        //act
        _movieService.AddMovie(_movieDTO);
        _movieService.AddMovie(_movieDTO);
        //assert
    }

    [TestMethod]
    public void AddMovie_WhenAddAMovie_ThenReturnSuccessfully()
    {
        //arrange
        //act
        _movieService.AddMovie(_movieDTO);
        //assert
        Movie? retrievedMovie = _inMemoryDatabase.GetMovie(_movie.Title);
        Assert.IsNotNull(retrievedMovie);
        Assert.IsTrue(_inMemoryDatabase.GetMovies().Contains(retrievedMovie));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetMovie_WhenGetAnUndefinedMovie_ThenThrowException()
    {
        //arrange
        //act
        _movieService.GetMovie("Sing Sing");
        //assert
    }

    [TestMethod]
    public void GetMovie_WhenGetAnExistentMovie_ThenTheMovieIsReturned()
    {
        //arrange
        _inMemoryDatabase.AddMovie(_movie);
        //act
        MovieDTO retrievedMovie = _movieService.GetMovie(_movie.Title);
        //assert
        Assert.AreEqual(retrievedMovie.Title, _movie.Title);
        Assert.AreEqual(retrievedMovie.Director, _movie.Director);
        Assert.AreEqual(retrievedMovie.ReleaseDate, _movie.ReleaseDate);
    }

    [TestMethod]
    public void GetMovies_WhenGetAllMoviesAndThereAreNoMovies_ThenNoMoviesAreReturned()
    {
        //arrange
        _inMemoryDatabase.GetMovies().Clear();
        //act
        List<MovieDTO> movies = _movieService.GetMovies();
        //assert
        Assert.IsTrue(movies.Count == 0);
    }

    [TestMethod]
    public void GetMovies_WhenGetAllMovies_ThenAllMoviesAreReturned()
    {
        //arrange
        _inMemoryDatabase.GetMovies().Clear();
        _inMemoryDatabase.AddMovie(_movie);
        //act
        List<MovieDTO> movies = _movieService.GetMovies();
        //assert
        Assert.IsTrue(movies.Count == 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteMovie_WhenDeleteAnUndefinedMovie_ThenThrowException()
    {
        //arrange
        //act
        _movieService.DeleteMovie("Sing Sing");
        //assert
    }

    [TestMethod]
    public void DeleteMovie_WhenDeleteAnExistentMovie_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.GetMovies().Clear();
        _inMemoryDatabase.AddMovie(_movie);
        //act
        _movieService.DeleteMovie(_movie.Title);
        //assert
        Assert.IsTrue(_inMemoryDatabase.GetMovies().Count == 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateMovie_WhenUpdateAnUndefinedMovie_ThenThrowException()
    {
        //arrange
        //act
        _movieService.UpdateMovie(_movieDTO);
        //assert
    }

    [TestMethod]
    public void UpdateMovie_WhenUpdateAMovie_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.GetMovies().Clear();
        _inMemoryDatabase.AddMovie(_movie);
        _movieDTO.Director = "Antoine Fuqua";
        _movieDTO.Budget = 1000000;
        //act
        _movieService.UpdateMovie(_movieDTO);
        //assert
        Movie? updatedMovie = _inMemoryDatabase.GetMovie(_movieDTO.Title);
        Assert.AreEqual(_movieDTO.Title, updatedMovie.Title);
        Assert.AreEqual(_movieDTO.Director, updatedMovie.Director);
        Assert.AreEqual(_movieDTO.Budget, updatedMovie.Budget);
    }
}