namespace Domain.Tests;

[TestClass]
public class UserTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenNameIsEmpty_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User("", "test@email.com", "P@ssw0rd.");
    }

    [TestMethod]
    public void NewUser_WhenNameIsValid_ThenNameIsAssigned()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "username@email.com", "P@ssw0rd.");
        //assert
        Assert.AreEqual("Username", user.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenEmailIsEmpty_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "", "P@ssw0rd.");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenEmailIsInvalid_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "@domain.com", "P@ssw0rd_");
    }

    [TestMethod]
    public void NewUser_WhenEmailIsValid_ThenEmailIsAssigned()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "username@domain.com", "P@ssw0rd$");
        //assert
        Assert.AreEqual("username@domain.com", user.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenPasswordIsEmpty_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "email@domain.com", "");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenPasswordLengthIsInvalid_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "email@domain.com", "pass.");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenPasswordDoesNotHaveAnySpecialChar_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "email@domain.com", "passwd");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenPasswordLengthIsValidButDoesNotHaveAnySpecialChar_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "email@domain.com", "Password1");
    }

    [TestMethod]
    public void NewUser_WhenPasswordIsValid_ThenPasswordIsAssigned()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "username@email.com", "P@ssw0rd_");
        //assert
        Assert.AreEqual("P@ssw0rd_", user.Password);
    }
}