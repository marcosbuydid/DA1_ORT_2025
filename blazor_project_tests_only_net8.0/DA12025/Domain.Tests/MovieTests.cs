namespace Domain.Tests;

[TestClass]
public class MovieTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewMovie_WhenTitleIsNull_ThenThrowsException()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie(null, "Ridley Scott", new DateTime(2024, 11, 22), 250000000);
        //assert
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewMovie_WhenTitleIsEmpty_ThenThrowsException()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie("", "Ridley Scott", new DateTime(2024, 11, 22), 250000000);
        //assert
    }

    [TestMethod]
    public void CreateNewMovie_WhenTitleIsNotNullOrEmpty_ThenTitleIsAssigned()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie("Gladiator 2", "Ridley Scott", new DateTime(2024, 11, 22), 250000000);
        //assert
        Assert.AreEqual("Gladiator 2", movie.Title);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewMovie_WhenDirectorIsNull_ThenThrowsException()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie("Gladiator 2", null, new DateTime(2024, 11, 22), 250000000);
        //assert
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewMovie_WhenDirectorIsEmpty_ThenThrowsException()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie("Gladiator 2", "", new DateTime(2024, 11, 22), 250000000);
        //assert
    }

    [TestMethod]
    public void CreateNewMovie_WhenDirectorIsNotNullOrEmpty_ThenDirectorIsAssigned()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie("Gladiator 2", "Ridley Scott", new DateTime(2024, 11, 22), 250000000);
        //assert
        Assert.AreEqual("Ridley Scott", movie.Director);
    }

    [TestMethod]
    public void CreateNewMovie_WhenReleaseDateIsEarlierThanToday_ThenReleaseDateIsAssigned()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie("Gladiator 2", "Ridley Scott", new DateTime(2024, 11, 22), 250000000);
        //assert
        Assert.AreEqual(new DateTime(2024, 11, 22), movie.ReleaseDate);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewMovie_WhenBudgetIsNegative_ThenThrowsException()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie("Gladiator 2", "Ridley Scott", new DateTime(2024, 11, 22), -250000000);
        //assert
    }

    [TestMethod]
    public void CreateNewMovie_WhenBudgetIsPositive_ThenBudgetIsAssigned()
    {
        //arrange
        Movie movie;
        //act
        movie = new Movie("Gladiator 2", "Ridley Scott", new DateTime(2024, 11, 22), 250000000);
        //assert
        Assert.AreEqual(250000000, movie.Budget);
    }
}