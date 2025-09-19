using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class UserServiceTests
{
    private AppDbContext _context;
    private InMemoryAppContextFactory _contextFactory;
    private UserRepository _userRepository;
    private UserService _userService;
    private User _user;
    private UserDTO _userDto;

    [TestInitialize]
    public void SetUp()
    {
        _contextFactory = new InMemoryAppContextFactory();
        _context = _contextFactory.CreateDbContext();
        _userRepository = new UserRepository(_context);
        _userService = new UserService(_userRepository);
        _user = new User(1, "Tim", "Robbins", "timrobbins@email.com", "123456", "User");
        _userDto = new UserDTO(1, "Tim", "Robbins", "timrobbins@email.com", "123456", "User");
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void GetUsers_WhenCalled_ThenUsersAreReturned()
    {
        //arrange
        _userRepository.AddUser(_user);
        //act
        List<UserDTO> users = _userService.GetUsers();
        //assert
        Assert.IsTrue(users.Exists(u => u.Email == "timrobbins@email.com"));
        Assert.AreEqual(1, users.Count);
    }

    [TestMethod]
    public void AddUser_WhenCalled_ThenUserIsAdded()
    {
        //arrange
        //act
        _userService.AddUser(_userDto);
        //assert
        User? addedUser = _userRepository.GetUsers().FirstOrDefault(u => u.Email == _userDto.Email);
        Assert.IsNotNull(addedUser);
        Assert.AreEqual(_userDto.Name, addedUser.Name);
        Assert.AreEqual(_userDto.LastName, addedUser.LastName);
        Assert.AreEqual(_userDto.Email, addedUser.Email);
        Assert.AreEqual(_userDto.Password, addedUser.Password);
        Assert.AreEqual(_userDto.Role, addedUser.Role);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddUser_WhenCalledTwiceWithTheSameUser_ThenThrowsException()
    {
        //arrange
        //act
        _userService.AddUser(_userDto);
        _userService.AddUser(_userDto);
        //assert
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
        _userRepository.AddUser(_user);
        //act
        UserDTO result = _userService.GetUser(_user.Email);
        //assert
        Assert.IsNotNull(result);
        Assert.AreEqual(_user.Name, result.Name);
        Assert.AreEqual(_user.LastName, result.LastName);
        Assert.AreEqual(_user.Email, result.Email);
        Assert.AreEqual(_user.Role, result.Role);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteUser_WhenCalledWithUserThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        //act
        _userService.DeleteUser("user email");
        //assert
    }

    [TestMethod]
    public void DeleteUser_WhenCalled_ThenUserIsDeleted()
    {
        //arrange
        _userRepository.AddUser(_user);
        //act
        _userService.DeleteUser(_user.Email);
        //assert
        Assert.AreEqual(0, _userRepository.GetUsers().Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateMovie_WhenCalledWithUserThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        //act
        _userService.UpdateUser(_userDto);
        //assert
    }

    [TestMethod]
    public void UpdateUser_WhenCalled_ThenUserIsUpdated()
    {
        //arrange
        _userService.AddUser(_userDto);
        _userDto.Name = "New Name";
        _userDto.LastName = "New LastName";
        _userDto.Role = "New Role";
        //act
        _userService.UpdateUser(_userDto);
        //assert
        User updatedUser = _userRepository.GetUsers().First();
        Assert.AreEqual("New Name", updatedUser.Name);
        Assert.AreEqual("New LastName", updatedUser.LastName);
        Assert.AreEqual("New Role", updatedUser.Role);
    }
}