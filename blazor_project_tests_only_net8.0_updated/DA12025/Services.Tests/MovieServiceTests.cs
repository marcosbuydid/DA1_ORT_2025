using DataAccess;
using Domain;

namespace Services.Tests;

[TestClass]
public class MovieServiceTests
{
    private InMemoryMovieRepository _movieRepository;
    private MovieService _movieService;
    private Movie _movie;

    [TestInitialize]
    public void Setup()
    {
        _movieRepository = new InMemoryMovieRepository();
        _movieService = new MovieService(_movieRepository);
        _movie = new Movie("Sing Sing", "Greg Kwedar", new DateTime(2024, 07, 12), 2000000);
    }

    [TestMethod]
    public void AddMovie_WhenCalledTwiceWithTheSameMovie_ThenThrowsException()
    {
        //arrange
        //act
        _movieService.AddMovie(_movie);
        //assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movieService.AddMovie(_movie));
        Assert.AreEqual("There`s a movie already defined with that title", exception.Message);
    }

    [TestMethod]
    public void AddMovie_WhenCalled_ThenMovieIsAdded()
    {
        //arrange
        //act
        _movieService.AddMovie(_movie);
        //assert
        Movie? retrievedMovie = _movieRepository.GetMovie(_movie.Title);
        Assert.IsNotNull(retrievedMovie);
        Assert.IsTrue(_movieRepository.GetMovies().Count == 1);
    }

    [TestMethod]
    public void GetMovie_WhenCalledWithMovieThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        //act and assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movieService.GetMovie("Sing Sing"));
        Assert.AreEqual("Cannot find movie with this title", exception.Message);
    }

    [TestMethod]
    public void GetMovie_WhenCalled_ThenMovieIsReturned()
    {
        //arrange
        _movieRepository.AddMovie(_movie);
        //act
        Movie retrievedMovie = _movieService.GetMovie(_movie.Title);
        //assert
        Assert.AreSame(retrievedMovie, _movie);
    }

    [TestMethod]
    public void GetMovies_WhenCalledWithNoMoviesInRepository_ThenNoMoviesAreReturned()
    {
        //arrange
        //act
        _movieService.GetMovies();
        //assert
        Assert.IsTrue(_movieRepository.GetMovies().Count == 0);
    }

    [TestMethod]
    public void GetMovies_WhenCalled_ThenMoviesAreReturned()
    {
        //arrange
        _movieRepository.AddMovie(_movie);
        //act
        _movieService.GetMovies();
        //assert
        Assert.IsTrue(_movieRepository.GetMovies().Contains(_movie));
        Assert.IsTrue(_movieRepository.GetMovies().Count == 1);
    }

    [TestMethod]
    public void DeleteMovie_WhenCalledWithMovieThatDoesNotExists_ThenThrowsException()
    {
        //arrange
        //act and assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movieService.DeleteMovie("Sing Sing"));
        Assert.AreEqual("Cannot find movie with this title", exception.Message);
    }

    [TestMethod]
    public void DeleteMovie_WhenCalled_ThenMovieIsDeleted()
    {
        //arrange
        _movieRepository.AddMovie(_movie);
        //act
        _movieService.DeleteMovie(_movie.Title);
        //assert
        Assert.IsTrue(_movieRepository.GetMovies().Count == 0);
    }

    [TestMethod]
    public void UpdateMovie_WhenCalledWithMovieThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        //act and assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movieService.UpdateMovie(_movie));
        Assert.AreEqual("Cannot find the specified movie", exception.Message);
    }

    [TestMethod]
    public void UpdateMovie_WhenCalled_ThenMovieIsUpdated()
    {
        //arrange
        _movieRepository.AddMovie(_movie);
        _movie.Budget = 1000000;
        //act
        _movieService.UpdateMovie(_movie);
        //assert
        Movie? retrievedMovie = _movieRepository.GetMovie(_movie.Title);
        Assert.AreSame(retrievedMovie, _movie);
        Assert.AreEqual(1000000, _movie.Budget);
    }
}