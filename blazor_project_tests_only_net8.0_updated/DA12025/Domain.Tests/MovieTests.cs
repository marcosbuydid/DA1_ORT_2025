namespace Domain.Tests;

[TestClass]
public class MovieTests
{
    private Movie _movie;

    [TestInitialize]
    public void Initialize()
    {
        _movie = new Movie("Gladiator 2", "Ridley Scott", new DateTime(2024, 11, 22), 250000000);
    }
    
    [TestMethod]
    public void CreateNewMovie_WhenTitleIsNull_ThenThrowsException()
    {
        //arrange
        //act and assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movie.Title = null);
        Assert.AreEqual("Title cannot be null or empty", exception.Message);
    }
    
    [TestMethod]
    public void CreateNewMovie_WhenTitleIsEmpty_ThenThrowsException()
    {
        //arrange
        //act and assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movie.Title = "");
        Assert.AreEqual("Title cannot be null or empty", exception.Message);
    }

    [TestMethod]
    public void CreateNewMovie_WhenTitleIsNotNullOrEmpty_ThenTitleIsAssigned()
    {
        //arrange
        //act
        //assert
        Assert.AreEqual("Gladiator 2", _movie.Title);
    }

    [TestMethod]
    public void CreateNewMovie_WhenDirectorIsNull_ThenThrowsException()
    {
        //arrange
        //act and assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movie.Director = null);
        Assert.AreEqual("Director cannot be null or empty", exception.Message);
    }
    
    [TestMethod]
    public void CreateNewMovie_WhenDirectorIsEmpty_ThenThrowsException()
    {
        //arrange
        //act and assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movie.Director = "");
        Assert.AreEqual("Director cannot be null or empty", exception.Message);
    }

    [TestMethod]
    public void CreateNewMovie_WhenDirectorIsNotNullOrEmpty_ThenDirectorIsAssigned()
    {
        //arrange
        //act
        //assert
        Assert.AreEqual("Ridley Scott", _movie.Director);
    }

    [TestMethod]
    public void CreateNewMovie_WhenReleaseDateIsEarlierThanToday_ThenReleaseDateIsAssigned()
    {
        //arrange
        //act
        //assert
        Assert.AreEqual(new DateTime(2024, 11, 22), _movie.ReleaseDate);
    }

    [TestMethod]
    public void CreateNewMovie_WhenBudgetIsNegative_ThenThrowsException()
    {
        //arrange
        //act and assert
        Exception exception = Assert.ThrowsException<ArgumentException>(() => _movie.Budget = -130000000);
        Assert.AreEqual("Budget must be a positive number", exception.Message);
    }

    [TestMethod]
    public void CreateNewMovie_WhenBudgetIsPositive_ThenBudgetIsAssigned()
    {
        //arrange
        //act
        //assert
        Assert.AreEqual(250000000, _movie.Budget);
    }
}