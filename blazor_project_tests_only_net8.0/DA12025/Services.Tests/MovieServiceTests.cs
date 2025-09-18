using DataAccess;
using Domain;

namespace Services.Tests;

[TestClass]
public class MovieServiceTests
{
    private InMemoryDatabase _inMemoryDatabase;
    private MovieService _movieService;
    private Movie _movie;

    [TestInitialize]
    public void Setup()
    {
        _inMemoryDatabase = new InMemoryDatabase();
        _movieService = new MovieService(_inMemoryDatabase);
        _movie = new Movie("Sing Sing", "Greg Kwedar", new DateTime(2024, 07, 12), 2000000);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddMovie_WhenAddTheSameMovieTwice_ThenThrowException()
    {
        //arrange
        //act
        _movieService.AddMovie(_movie);
        _movieService.AddMovie(_movie);
        //assert
    }

    [TestMethod]
    public void AddMovie_WhenAddAMovie_ThenReturnSuccessfully()
    {
        //arrange
        //act
        _movieService.AddMovie(_movie);
        //assert
        Movie? retrievedMovie = _inMemoryDatabase.GetMovie(_movie.Title);
        Assert.IsNotNull(retrievedMovie);
        Assert.IsTrue(_inMemoryDatabase.GetMovies().Count == 1);
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
        Movie retrievedMovie = _movieService.GetMovie(_movie.Title);
        //assert
        Assert.AreSame(retrievedMovie, _movie);
    }

    [TestMethod]
    public void GetMovies_WhenGetAllMoviesAndThereAreNoMovies_ThenNoMoviesAreReturned()
    {
        //arrange
        //act
        _movieService.GetMovies();
        //assert
        Assert.IsTrue(_inMemoryDatabase.GetMovies().Count == 0);
    }

    [TestMethod]
    public void GetMovies_WhenGetAllMoviesAndThereAreSomeMovies_ThenAllMoviesAreReturned()
    {
        //arrange
        _inMemoryDatabase.AddMovie(_movie);
        //act
        _movieService.GetMovies();
        //assert
        Assert.IsTrue(_inMemoryDatabase.GetMovies().Contains(_movie));
        Assert.IsTrue(_inMemoryDatabase.GetMovies().Count == 1);
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
    public void DeleteMovie_WhenDeleteAMovie_ThenReturnSuccessfully()
    {
        //arrange
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
        _movieService.UpdateMovie(_movie);
        //assert
    }

    [TestMethod]
    public void UpdateMovie_WhenUpdateAMovie_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.AddMovie(_movie);
        _movie.Budget = 1000000;
        //act
        _movieService.UpdateMovie(_movie);
        //assert
        Movie? retrievedMovie = _inMemoryDatabase.GetMovie(_movie.Title);
        Assert.AreSame(retrievedMovie, _movie);
        Assert.AreEqual(1000000, _movie.Budget);
    }
}