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
    public void Add_WhenAddIsInvokedWithAnEmptyMovie_ThenThrowException()
    {
        //arrange
        //act
        _movieRepository.Add(new Movie());
    }

    [TestMethod]
    public void Add_WhenAddIsInvoked_ThenTheMovieIsAdded()
    {
        //arrange
        //act
        _movieRepository.Add(_movie);
        //assert
        IList<Movie> movies = _movieRepository.GetAll();
        Assert.AreEqual(1, movies.Count);
        Assert.AreEqual("Black Rain", movies[0].Title);
    }

    [TestMethod]
    public void GetAll_WhenGetAllIsInvoked_ThenAllMoviesAreReturned()
    {
        //arrange
        _movieRepository.Add(_movie);
        _movieRepository.Add(_movie_two);
        //act
        IList<Movie> movies = _movieRepository.GetAll();
        //assert
        Assert.AreEqual(2, movies.Count);
    }

    [TestMethod]
    public void Get_WhenGetIsInvoked_ThenTheMovieIsReturned()
    {
        //arrange
        _movieRepository.Add(_movie);
        //act
        Movie? result = _movieRepository.Get(m => m.Id == 1);
        //assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Black Rain", result.Title);
        Assert.AreEqual("Ridley Scott", result.Director);
        Assert.AreEqual(30000000, result.Budget);
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void Delete_WhenDeleteIsInvokedAndMovieNotExist_ThenThrowException()
    {
        //arrange
        Movie aMovie = new Movie(990, "Unknown Movie", "ADirector", DateTime.Now, 1000);
        //act
        _movieRepository.Delete(_movie);
    }

    [TestMethod]
    public void Delete_WhenDeleteIsInvoked_ThenTheMovieIsRemoved()
    {
        //arrange
        _movieRepository.Add(_movie);
        //act
        _movieRepository.Delete(_movie);
        //assert
        IList<Movie> result = _movieRepository.GetAll();
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void Update_WhenUpdateIsInvokedWithADetachedMovie_ThenThrowException()
    {
        //arrange
        Movie aMovie = new Movie(99, "Unknown Movie", "ADirector", DateTime.Now, 100);
        //we simulate the movie is not tracked
        //act
        _movieRepository.Update(aMovie);
        //it tries to update an object is not in the context
    }

    [TestMethod]
    public void Update_WhenUpdateIsInvoked_ThenTheMovieIsUpdated()
    {
        //arrange
        _movieRepository.Add(_movie);
        _movie.Title = "Updated Title";
        _movie.Director = "Updated Director";
        //act
        _movieRepository.Update(_movie);
        //assert
        Movie movieUpdated = _movieRepository.Get(m => m.Id == 1);
        Assert.AreEqual("Updated Title", movieUpdated.Title);
        Assert.AreEqual("Updated Director", movieUpdated.Director);
    }
}