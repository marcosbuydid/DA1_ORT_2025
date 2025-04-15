namespace Domain.Tests;

[TestClass]
public class UserTests
{
    [TestMethod]
    public void NewUser_WhenConstructorIsNotEmpty_ThenUserIsCreated()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.IsNotNull(user);
    }

    [TestMethod]
    public void CreateNewUser_WhenIdIsNotNull_ThenIdShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.AreEqual(1, user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenNameIsNull_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, null, "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenNameIsEmpty_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenNameIsNotNullOrEmpty_ThenNameShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.AreEqual("Nick", user.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenLastNameIsNull_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", null, "nickwilliams@email.com", "p@assword", "User");
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenLastNameIsEmpty_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "", "nickwilliams@email.com", "p@assword", "User");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenLastNameIsNotNullOrEmpty_ThenLastNameShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.AreEqual("Williams", user.LastName);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenEmailIsNull_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", null, "p@assword", "User");
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenEmailIsEmpty_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "", "p@assword", "User");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenEmailIsNotNullOrEmpty_ThenEmailShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.AreEqual("nickwilliams@email.com", user.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenPasswordIsNull_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", null, "User");
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenPasswordIsEmpty_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "", "User");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenPasswordIsNotNullOrEmpty_ThenPasswordShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.AreEqual("p@assword", user.Password);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenRoleIsNull_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", null);
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenRoleIsEmpty_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenRoleIsNotNullOrEmpty_ThenRoleIsShouldBeAssigned()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.AreEqual("User", user.Role);
    }
}