using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests;

[TestClass]
public class MovieRepositoryTests
{
    private AppDbContext _context;
    private InMemoryAppContextFactory _contextFactory;
    private MovieRepository _movieRepository;
    private Movie _movie;
    private Movie _movie_two;

    [TestInitialize]
    public void SetUp()
    {
        _contextFactory = new InMemoryAppContextFactory();
        _context = _contextFactory.CreateDbContext();
        _movieRepository = new MovieRepository(_context);
        _movie = new Movie(1, "Black Rain", "Ridley Scott", new DateTime(1989, 9, 22), 30000000);
        _movie_two = new Movie(2, "Interstellar", "Cristopher Nolan", new DateTime(2014, 11, 7), 165000000);
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void AddMovie_WhenCalledWithEmptyMovie_ThenThrowsException()
    {
        //arrange
        //act
        _movieRepository.AddMovie(new Movie());
    }

    [TestMethod]
    public void AddMovie_WhenCalled_ThenMovieIsAdded()
    {
        //arrange
        //act
        _movieRepository.AddMovie(_movie);
        //assert
        List<Movie> movies = _movieRepository.GetMovies();
        Assert.AreEqual(1, movies.Count);
        Assert.AreEqual("Black Rain", movies[0].Title);
    }

    [TestMethod]
    public void GetMovies_WhenCalled_ThenMoviesAreReturned()
    {
        //arrange
        _movieRepository.AddMovie(_movie);
        _movieRepository.AddMovie(_movie_two);
        //act
        List<Movie> movies = _movieRepository.GetMovies();
        //assert
        Assert.AreEqual(2, movies.Count);
    }

    [TestMethod]
    public void GetMovie_WhenCalled_ThenMovieIsReturned()
    {
        //arrange
        _movieRepository.AddMovie(_movie);
        //act
        Movie? result = _movieRepository.GetMovie(m => m.Id == 1);
        //assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Black Rain", result.Title);
        Assert.AreEqual("Ridley Scott", result.Director);
        Assert.AreEqual(30000000, result.Budget);
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void DeleteMovie_WhenCalledWithMovieThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        Movie aMovie = new Movie(990, "Unknown Movie", "ADirector", DateTime.Now, 1000);
        //act
        _movieRepository.DeleteMovie(_movie);
    }

    [TestMethod]
    public void DeleteMovie_WhenCalled_ThenMovieIsDeleted()
    {
        //arrange
        _movieRepository.AddMovie(_movie);
        //act
        _movieRepository.DeleteMovie(_movie);
        //assert
        List<Movie> result = _movieRepository.GetMovies();
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void UpdateMovie_WhenCalledWithDetachedMovie_ThenThrowsException()
    {
        //arrange
        //we simulate the movie is not tracked
        Movie aMovie = new Movie(99, "Unknown Movie", "ADirector", DateTime.Now, 100);
        //act
        //it tries to update an object that`s not in the context
        _movieRepository.UpdateMovie(aMovie);
    }

    [TestMethod]
    public void UpdateMovie_WhenCalled_ThenMovieIsUpdated()
    {
        //arrange
        _movieRepository.AddMovie(_movie);
        _movie.Title = "Updated Title";
        _movie.Director = "Updated Director";
        //act
        _movieRepository.UpdateMovie(_movie);
        //assert
        Movie? movieUpdated = _movieRepository.GetMovie(m => m.Id == 1);
        Assert.AreEqual("Updated Title", movieUpdated.Title);
        Assert.AreEqual("Updated Director", movieUpdated.Director);
    }
}