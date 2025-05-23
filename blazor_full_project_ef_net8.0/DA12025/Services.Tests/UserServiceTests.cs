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
        _userDto = new UserDTO();
        _userDto.Id = 1;
        _userDto.Name = "Tim";
        _userDto.LastName = "Robbins";
        _userDto.Email = "timrobbins@email.com";
        _userDto.Password = "123456";
        _userDto.Role = "User";
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void GetUsers_WhenGetUsersIsInvoked_ThenAllUsersAreReturned()
    {
        //arrange
        _userRepository.Add(_user);
        //act
        List<UserDTO> users = _userService.GetUsers();
        //assert
        Assert.IsTrue(users.Exists(u => u.Email == "timrobbins@email.com"));
        Assert.AreEqual(1, users.Count);
    }

    [TestMethod]
    public void AddUser_WhenAddUserIsInvoked_ThenTheUserIsAdded()
    {
        //arrange
        //act
        _userService.AddUser(_userDto);
        //assert
        User? addedUser = _userRepository.GetAll().FirstOrDefault(u => u.Email == _userDto.Email);
        Assert.IsNotNull(addedUser);
        Assert.AreEqual(_userDto.Name, addedUser.Name);
        Assert.AreEqual(_userDto.LastName, addedUser.LastName);
        Assert.AreEqual(_userDto.Email, addedUser.Email);
        Assert.AreEqual(_userDto.Password, addedUser.Password);
        Assert.AreEqual(_userDto.Role, addedUser.Role);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddUser_WhenAddTheSameUserTwice_ThenThrowException()
    {
        //arrange
        //act
        _userService.AddUser(_userDto);
        _userService.AddUser(_userDto);
        //assert
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
    public void GetUser_WhenGetAValidUser_ThenTheUserIsReturned()
    {
        //arrange
        _userRepository.Add(_user);
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
    [ExpectedException(typeof(ArgumentNullException))]
    public void DeleteUser_WhenDeleteAnUndefinedUser_ThenThrowException()
    {
        //arrange
        //act
        _userService.DeleteUser("user email");
        //assert
    }

    [TestMethod]
    public void DeleteUser_WhenDeleteAValidUser_ThenTheUserIsDeleted()
    {
        //arrange
        _userRepository.Add(_user);
        //act
        _userService.DeleteUser(_user.Email);
        //assert
        Assert.AreEqual(0, _userRepository.GetAll().Count);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void UpdateMovie_WhenUpdateAnInvalidUser_ThenThrowException()
    {
        //arrange
        //act
        _userService.UpdateUser(_userDto);
        //assert
    }

    [TestMethod]
    public void UpdateUser_WhenUpdateAValidUser_ThenTheUserIsUpdated()
    {
        //arrange
        _userService.AddUser(_userDto);
        _userDto.Name = "New Name";
        _userDto.LastName = "New LastName";
        _userDto.Role = "New Role";
        //act
        _userService.UpdateUser(_userDto);
        //assert
        User updatedUser = _userRepository.GetAll().First();
        Assert.AreEqual("New Name", updatedUser.Name);
        Assert.AreEqual("New LastName", updatedUser.LastName);
        Assert.AreEqual("New Role", updatedUser.Role);
    }
}