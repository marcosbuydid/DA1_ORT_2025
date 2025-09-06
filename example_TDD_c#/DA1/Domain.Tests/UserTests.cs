namespace Domain.Tests;

[TestClass]
public class UserTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenNameIsEmpty_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User("", "test@email.com", "P@ssw0rd");
    }

    [TestMethod]
    public void NewUser_WhenNameIsValid_ThenNameShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "username@email.com", "P@ssw0rd");
        //assert
        Assert.AreEqual("Username", user.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenEmailIsEmpty_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "", "P@ssw0rd");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenEmailIsNotValid_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "@domain.com", "P@ssw0rd");
    }

    [TestMethod]
    public void NewUser_WhenEmailIsValid_ThenEmailShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "username@domain.com", "P@ssw0rd");
        //assert
        Assert.AreEqual("username@domain.com", user.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenPasswordIsEmpty_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "email@domain.com", "");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewUser_WhenPasswordIsLessThanEightCharsLong_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "email@domain.com", "passwd");
    }

    [TestMethod]
    public void NewUser_WhenPasswordIsValid_ThenPasswordShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User("Username", "username@email.com", "P@ssw0rd");
        //assert
        Assert.AreEqual("P@ssw0rd", user.Password);
    }
}