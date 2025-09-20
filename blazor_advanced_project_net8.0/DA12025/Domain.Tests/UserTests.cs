namespace Domain.Tests;

[TestClass]
public class UserTests
{
    [TestMethod]
    public void CreateNewUser_WhenConstructorIsNotEmpty_ThenUserIsCreated()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.IsNotNull(user);
    }

    [TestMethod]
    public void CreateNewUser_WhenIdIsNotNull_ThenIdIsAssigned()
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
    public void CreateNewUser_WhenNameIsNull_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, null, "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenNameIsEmpty_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenNameIsNotNullOrEmpty_ThenNameIsAssigned()
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
    public void CreateNewUser_WhenLastNameIsNull_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", null, "nickwilliams@email.com", "p@assword", "User");
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenLastNameIsEmpty_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "", "nickwilliams@email.com", "p@assword", "User");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenLastNameIsNotNullOrEmpty_ThenLastNameIsAssigned()
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
    public void CreateNewUser_WhenEmailIsNull_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", null, "p@assword", "User");
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenEmailIsEmpty_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "", "p@assword", "User");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenEmailIsNotNullOrEmpty_ThenEmailIsAssigned()
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
    public void CreateNewUser_WhenPasswordIsNull_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", null, "User");
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenPasswordIsEmpty_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "", "User");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenPasswordIsNotNullOrEmpty_ThenPasswordIsAssigned()
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
    public void CreateNewUser_WhenRoleIsNull_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", null);
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenRoleIsEmpty_ThenThrowsException()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "");
        //assert
    }

    [TestMethod]
    public void CreateNewUser_WhenRoleIsNotNullOrEmpty_ThenRoleIsAssigned()
    {
        //arrange
        User user;
        //act
        user = new User(1, "Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.AreEqual("User", user.Role);
    }
}