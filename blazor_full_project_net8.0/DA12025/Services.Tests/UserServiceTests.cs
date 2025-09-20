using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class UserServiceTests
{
    private InMemoryDatabase _inMemoryDatabase;
    private UserService _userService;
    private User _user;
    private UserDTO _userDTO;

    [TestInitialize]
    public void Setup()
    {
        _inMemoryDatabase = new InMemoryDatabase();
        _userService = new UserService(_inMemoryDatabase);
        _user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        _userDTO = new UserDTO("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddUser_WhenCalledTwiceWithTheSameUser_ThenThrowsException()
    {
        //arrange
        _inMemoryDatabase.GetUsers().Clear();
        //act
        _userService.AddUser(_userDTO);
        _userService.AddUser(_userDTO);
        //assert
    }

    [TestMethod]
    public void AddUser_WhenCalled_ThenUserIsAdded()
    {
        //arrange
        //act
        _userService.AddUser(_userDTO);
        //assert
        User? retrievedUser = _inMemoryDatabase.GetUser(_userDTO.Email);
        Assert.IsNotNull(retrievedUser);
        Assert.IsTrue(_inMemoryDatabase.GetUsers().Contains(retrievedUser));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetUser_WhenCalledWithUserThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        //act
        _userService.GetUser("email@email.com");
        //assert
    }

    [TestMethod]
    public void GetUser_WhenCalled_ThenUserIsReturned()
    {
        //arrange
        _inMemoryDatabase.AddUser(_user);
        //act
        UserDTO retrievedUser = _userService.GetUser(_userDTO.Email);
        //assert
        Assert.AreEqual(retrievedUser.Name, _user.Name);
        Assert.AreEqual(retrievedUser.LastName, _user.LastName);
        Assert.AreEqual(retrievedUser.Email, _user.Email);
        Assert.AreEqual(retrievedUser.Role, _user.Role);
    }

    [TestMethod]
    public void GetUsers_WhenCalledWithNoUsersCreated_ThenNoUsersAreReturned()
    {
        //arrange
        _inMemoryDatabase.GetUsers().Clear();
        //act
        List<UserDTO> users = _userService.GetUsers();
        //assert
        Assert.IsTrue(users.Count == 0);
    }

    [TestMethod]
    public void GetUsers_WhenCalled_ThenUsersAreReturned()
    {
        //arrange
        _inMemoryDatabase.GetUsers().Clear();
        _inMemoryDatabase.AddUser(_user);
        //act
        List<UserDTO> users = _userService.GetUsers();
        //assert
        Assert.IsTrue(users.Count == 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteUser_WhenCalledWithUserThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        //act
        _userService.DeleteUser("email@email.com");
        //assert
    }

    [TestMethod]
    public void DeleteUser_WhenCalled_ThenUserIsDeleted()
    {
        //arrange
        _inMemoryDatabase.GetUsers().Clear();
        _inMemoryDatabase.AddUser(_user);
        //act
        _userService.DeleteUser(_userDTO.Email);
        //assert
        Assert.IsTrue(_inMemoryDatabase.GetUsers().Count == 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateUser_WhenCalledWithUserThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        //act
        _userService.UpdateUser(_userDTO);
        //assert
    }

    [TestMethod]
    public void UpdateUser_WhenCalled_ThenUserIsUpdated()
    {
        //arrange
        _inMemoryDatabase.GetUsers().Clear();
        _inMemoryDatabase.AddUser(_user);
        _userDTO.Name = "Tom";
        _userDTO.LastName = "Clarks";
        //act
        _userService.UpdateUser(_userDTO);
        //assert
        User? updatedUser = _inMemoryDatabase.GetUser(_userDTO.Email);
        Assert.AreEqual(_userDTO.Name, updatedUser.Name);
        Assert.AreEqual(_userDTO.LastName, updatedUser.LastName);
        Assert.AreEqual(_userDTO.Email, updatedUser.Email);
        Assert.AreEqual(_userDTO.Password, updatedUser.Password);
        Assert.AreEqual(_userDTO.Role, updatedUser.Role);
    }
}