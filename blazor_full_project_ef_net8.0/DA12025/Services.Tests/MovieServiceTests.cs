using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class MovieServiceTests
{
    private AppDbContext _context;
    private InMemoryAppContextFactory _contextFactory;
    private MovieRepository _movieRepository;
    private MovieService _movieService;
    private Movie _movie;
    private MovieDTO _movieDto;

    [TestInitialize]
    public void SetUp()
    {
        _contextFactory = new InMemoryAppContextFactory();
        _context = _contextFactory.CreateDbContext();
        _movieRepository = new MovieRepository(_context);
        _movieService = new MovieService(_movieRepository);
        _movie = new Movie(1, "Black Rain", "Ridley Scott", new DateTime(1989, 9, 22), 30000000);
        _movieDto = new MovieDTO();
        _movieDto.Id = 1;
        _movieDto.Title = "Black Rain";
        _movieDto.Director = "Ridley Scott";
        _movieDto.ReleaseDate = new DateTime(1989, 9, 22);
        _movieDto.Budget = 30000000;
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void GetMovies_WhenGetMoviesIsInvoked_ThenAllMoviesAreReturned()
    {
        //arrange
        _movieRepository.Add(_movie);
        //act
        List<MovieDTO> movies = _movieService.GetMovies();
        //assert
        Assert.IsTrue(movies.Exists(m => m.Title == "Black Rain"));
        Assert.AreEqual(1, movies.Count);
    }

    [TestMethod]
    public void AddMovie_WhenAddMovieIsInvoked_ThenTheMovieIsAdded()
    {
        //arrange
        //act
        _movieService.AddMovie(_movieDto);
        //assert
        Movie? addedMovie = _movieRepository.GetAll().FirstOrDefault(m => m.Title == _movieDto.Title);
        Assert.IsNotNull(addedMovie);
        Assert.AreEqual(_movieDto.Title, addedMovie.Title);
        Assert.AreEqual(_movieDto.Director, addedMovie.Director);
        Assert.AreEqual(_movieDto.ReleaseDate, addedMovie.ReleaseDate);
        Assert.AreEqual(_movieDto.Budget, addedMovie.Budget);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddMovie_WhenAddTheSameMovieTwice_ThenThrowException()
    {
        //arrange
        //act
        _movieService.AddMovie(_movieDto);
        _movieService.AddMovie(_movieDto);
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetMovie_WhenGetAnUndefinedMovie_ThenThrowException()
    {
        //arrange
        //act
        _movieService.GetMovie("movie title");
        //assert
    }

    [TestMethod]
    public void GetMovie_WhenGetAValidMovie_ThenTheMovieIsReturned()
    {
        //arrange
        _movieRepository.Add(_movie);
        //act
        MovieDTO result = _movieService.GetMovie(_movie.Title);
        //assert
        Assert.IsNotNull(result);
        Assert.AreEqual(_movie.Title, result.Title);
        Assert.AreEqual(_movie.Director, result.Director);
        Assert.AreEqual(_movie.ReleaseDate, result.ReleaseDate);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteMovie_WhenDeleteAnUndefinedMovie_ThenThrowException()
    {
        //arrange
        //act
        _movieService.DeleteMovie("movie title");
        //assert
    }

    [TestMethod]
    public void DeleteMovie_WhenDeleteAValidMovie_ThenTheMovieIsDeleted()
    {
        //arrange
        _movieRepository.Add(_movie);
        //act
        _movieService.DeleteMovie(_movie.Title);
        //assert
        Assert.AreEqual(0, _movieRepository.GetAll().Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateMovie_WhenUpdateAnInvalidMovie_ThenThrowException()
    {
        //arrange
        //act
        _movieService.UpdateMovie(_movieDto);
        //assert
    }

    [TestMethod]
    public void UpdateMovie_WhenUpdateAValidMovie_ThenTheMovieIsUpdated()
    {
        //arrange
        _movieService.AddMovie(_movieDto);
        _movieDto.Director = "New Director";
        _movieDto.ReleaseDate = new DateTime(2000, 1, 1);
        //act
        _movieService.UpdateMovie(_movieDto);
        //assert
        Movie updatedMovie = _movieRepository.GetAll().First();
        Assert.AreEqual("New Director", updatedMovie.Director);
        Assert.AreEqual(new DateTime(2000, 1, 1), updatedMovie.ReleaseDate);
    }
}