using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class MovieServiceTests
{
    private InMemoryDatabase _inMemoryDatabase;
    private MovieService _movieService;
    private Movie movie;
    private MovieDTO movieDTO;

    [TestInitialize]
    public void Setup()
    {
        _inMemoryDatabase = new InMemoryDatabase();
        _movieService = new MovieService(_inMemoryDatabase);
        movie = new Movie("Sing Sing", "Greg Kwedar", new DateTime(2024, 07, 12), 2000000);
        movieDTO = new MovieDTO();
        movieDTO.Title = "Sing Sing";
        movieDTO.Director = "Greg Kwedar";
        movieDTO.ReleaseDate = DateTime.Today;
        movieDTO.Budget = 2000000;
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddNewMovie_WhenAddADuplicateMovie_ThenThrowException()
    {
        //arrange
        _inMemoryDatabase.Movies.Clear();
        //act
        _movieService.AddMovie(movieDTO);
        _movieService.AddMovie(movieDTO);
        //assert
    }

    [TestMethod]
    public void AddNewMovie_WhenAddMovie_ThenReturnSuccessfully()
    {
        //arrange
        //act
        _movieService.AddMovie(movieDTO);
        Movie? retrievedMovie = _inMemoryDatabase.Movies.Find(m => m.Title == movie.Title);
        //assert
        Assert.IsNotNull(retrievedMovie);
        Assert.IsTrue(_inMemoryDatabase.Movies.Contains(retrievedMovie));
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
    public void GetMovie_WhenGetAnExistentMovie_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.Movies.Add(movie);
        //act
        MovieDTO retrievedMovie = _movieService.GetMovie(movie.Title);
        //assert
        Assert.AreEqual(retrievedMovie.Title, movie.Title);
        Assert.AreEqual(retrievedMovie.Director, movie.Director);
        Assert.AreEqual(retrievedMovie.ReleaseDate, movie.ReleaseDate);
    }

    [TestMethod]
    public void GetMovies_WhenGettingAllMoviesAndThereIsNoMovies_ThenReturnZero()
    {
        //arrange
        _inMemoryDatabase.Movies.Clear();
        //act
        List<MovieDTO> movies = _movieService.GetMovies();
        //assert
        Assert.IsTrue(movies.Count == 0);
    }

    [TestMethod]
    public void GetMovies_WhenGettingAllMovies_ThenReturnAllMovies()
    {
        //arrange
        _inMemoryDatabase.Movies.Clear();
        _inMemoryDatabase.Movies.Add(movie);
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
    public void DeleteMovie_WhenDeleteAMovie_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.Movies.Clear();
        _inMemoryDatabase.Movies.Add(movie);
        //act
        _movieService.DeleteMovie(movie.Title);
        //assert
        Assert.IsTrue(_inMemoryDatabase.Movies.Count == 0);
    }

    [TestMethod]
    public void UpdateMovie_WhenUpdateAMovie_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.Movies.Clear();
        _inMemoryDatabase.Movies.Add(movie);
        movieDTO.Director = "Antoine Fuqua";
        movieDTO.Budget = 1000000;
        //act
        _movieService.UpdateMovie(movieDTO);
        Movie? updatedMovie = _inMemoryDatabase.Movies.Find(m => m.Title == movieDTO.Title);
        //assert
        Assert.AreEqual(movieDTO.Title, updatedMovie.Title);
        Assert.AreEqual(movieDTO.Director, updatedMovie.Director);
        Assert.AreEqual(movieDTO.Budget, updatedMovie.Budget);
    }
}