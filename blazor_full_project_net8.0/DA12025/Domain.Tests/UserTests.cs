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
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.IsNotNull(user);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewUser_WhenNameIsNull_ThenThrowException()
    {
        //arrange
        User user;
        //act
        user = new User(null, "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
    }
    
    [TestMethod]
    public void CreateNewUser_WhenNameIsNotNull_ThenNameIsValid()
    {
        //arrange
        User user;
        //act
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
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
        user = new User("Nick", null, "nickwilliams@email.com", "p@assword", "User");
        //assert
    }
    
    [TestMethod]
    public void CreateNewUser_WhenLastNameIsNotNull_ThenLastNameIsValid()
    {
        //arrange
        User user;
        //act
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
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
        user = new User("Nick", "Williams", null, "p@assword", "User");
        //assert
    }
    
    [TestMethod]
    public void CreateNewUser_WhenEmailIsNotNull_ThenEmailIsValid()
    {
        //arrange
        User user;
        //act
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
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
        user = new User("Nick", "Williams", "nickwilliams@email.com", null, "User");
        //assert
    }
    
    [TestMethod]
    public void CreateNewUser_WhenPasswordIsNotNull_ThenPasswordIsValid()
    {
        //arrange
        User user;
        //act
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
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
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", null);
        //assert
    }
    
    [TestMethod]
    public void CreateNewUser_WhenRoleIsNotNull_ThenRoleIsValid()
    {
        //arrange
        User user;
        //act
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        //assert
        Assert.AreEqual("User", user.Role);
    }
    
}