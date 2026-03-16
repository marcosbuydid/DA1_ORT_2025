using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class UserServiceTests
{
    private InMemoryUserRepository _inMemoryUserRepository;
    private UserService _userService;
    private User _user;
    private UserDTO _userDTO;

    [TestInitialize]
    public void Setup()
    {
        _inMemoryUserRepository = new InMemoryUserRepository();
        _userService = new UserService(_inMemoryUserRepository);
        _user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        _userDTO = new UserDTO("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddUser_WhenCalledTwiceWithTheSameUser_ThenThrowsException()
    {
        //arrange
        _inMemoryUserRepository.GetUsers().Clear();
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
        User? retrievedUser = _inMemoryUserRepository.GetUser(_userDTO.Email);
        Assert.IsNotNull(retrievedUser);
        Assert.IsTrue(_inMemoryUserRepository.GetUsers().Contains(retrievedUser));
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
        _inMemoryUserRepository.AddUser(_user);
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
        _inMemoryUserRepository.GetUsers().Clear();
        //act
        List<UserDTO> users = _userService.GetUsers();
        //assert
        Assert.IsTrue(users.Count == 0);
    }

    [TestMethod]
    public void GetUsers_WhenCalled_ThenUsersAreReturned()
    {
        //arrange
        _inMemoryUserRepository.GetUsers().Clear();
        _inMemoryUserRepository.AddUser(_user);
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
        _inMemoryUserRepository.GetUsers().Clear();
        _inMemoryUserRepository.AddUser(_user);
        //act
        _userService.DeleteUser(_userDTO.Email);
        //assert
        Assert.IsTrue(_inMemoryUserRepository.GetUsers().Count == 0);
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
        _inMemoryUserRepository.GetUsers().Clear();
        _inMemoryUserRepository.AddUser(_user);
        _userDTO.Name = "Tom";
        _userDTO.LastName = "Clarks";
        //act
        _userService.UpdateUser(_userDTO);
        //assert
        User? updatedUser = _inMemoryUserRepository.GetUser(_userDTO.Email);
        Assert.AreEqual(_userDTO.Name, updatedUser.Name);
        Assert.AreEqual(_userDTO.LastName, updatedUser.LastName);
        Assert.AreEqual(_userDTO.Email, updatedUser.Email);
        Assert.AreEqual(_userDTO.Password, updatedUser.Password);
        Assert.AreEqual(_userDTO.Role, updatedUser.Role);
    }
}