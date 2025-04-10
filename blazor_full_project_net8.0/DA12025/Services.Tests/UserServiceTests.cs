using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class UserServiceTests
{
    private InMemoryDatabase _inMemoryDatabase;
    private UserService _userService;
    private User user;
    private UserDTO userDTO;

    [TestInitialize]
    public void Setup()
    {
        _inMemoryDatabase = new InMemoryDatabase();
        _userService = new UserService(_inMemoryDatabase);
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        userDTO = new UserDTO();
        userDTO.Name = "Nick";
        userDTO.LastName = "Williams";
        userDTO.Email = "nickwilliams@email.com";
        userDTO.Password = "p@assword";
        userDTO.Role = "User";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddNewUser_WhenAddADuplicateUser_ThenThrowException()
    {
        //arrange
        _inMemoryDatabase.Users.Clear();
        //act
        _userService.AddUser(userDTO);
        _userService.AddUser(userDTO);
        //assert
    }

    [TestMethod]
    public void AddNewUser_WhenAddUser_ThenReturnSuccessfully()
    {
        //arrange
        //act
        _userService.AddUser(userDTO);
        User? retrievedUser = _inMemoryDatabase.Users.Find(u => u.Email == userDTO.Email);
        //assert
        Assert.IsNotNull(retrievedUser);
        Assert.IsTrue(_inMemoryDatabase.Users.Contains(retrievedUser));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetUser_WhenGetAnUndefinedUser_ThenThrowException()
    {
        //arrange
        //act
        _userService.GetUser("email@email.com");
        //assert
    }

    [TestMethod]
    public void GetUser_WhenGetAnExistentUser_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.Users.Add(user);
        //act
        UserDTO retrievedUser = _userService.GetUser(userDTO.Email);
        //assert
        Assert.AreEqual(retrievedUser.Name, user.Name);
        Assert.AreEqual(retrievedUser.LastName, user.LastName);
        Assert.AreEqual(retrievedUser.Email, user.Email);
        Assert.AreEqual(retrievedUser.Role, user.Role);
    }

    [TestMethod]
    public void GetUsers_WhenGettingAllUsersAndThereIsNoUsers_ThenReturnZero()
    {
        //arrange
        _inMemoryDatabase.Users.Clear();
        //act
        List<UserDTO> users = _userService.GetUsers();
        //assert
        Assert.IsTrue(users.Count == 0);
    }

    [TestMethod]
    public void GetUsers_WhenGettingAllUsers_ThenReturnAllUsers()
    {
        //arrange
        _inMemoryDatabase.Users.Clear();
        _inMemoryDatabase.Users.Add(user);
        //act
        List<UserDTO> users = _userService.GetUsers();
        //assert
        Assert.IsTrue(users.Count == 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteUser_WhenDeleteAnUndefinedUser_ThenThrowException()
    {
        //arrange
        //act
        _userService.DeleteUser("email@email.com");
        //assert
    }

    [TestMethod]
    public void DeleteUser_WhenDeleteAUser_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.Users.Clear();
        _inMemoryDatabase.Users.Add(user);
        //act
        _userService.DeleteUser(userDTO.Email);
        //assert
        Assert.IsTrue(_inMemoryDatabase.Users.Count == 0);
    }

    [TestMethod]
    public void UpdateUser_WhenUpdateAUser_ThenReturnSuccessfully()
    {
        //arrange
        _inMemoryDatabase.Users.Clear();
        _inMemoryDatabase.Users.Add(user);
        userDTO.Name = "Tom";
        userDTO.LastName = "Clarks";
        //act
        _userService.UpdateUser(userDTO);
        User? updatedUser = _inMemoryDatabase.Users.Find(u => u.Email == userDTO.Email);
        //assert
        Assert.AreEqual(userDTO.Name, updatedUser.Name);
        Assert.AreEqual(userDTO.LastName, updatedUser.LastName);
        Assert.AreEqual(userDTO.Email, updatedUser.Email);
        Assert.AreEqual(userDTO.Password, updatedUser.Password);
        Assert.AreEqual(userDTO.Role, updatedUser.Role);
    }
}