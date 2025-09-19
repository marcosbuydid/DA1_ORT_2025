using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class SessionServiceTests
{
    private AppDbContext _context;
    private InMemoryAppContextFactory _contextFactory;
    private UserRepository _userRepository;
    private UserService _userService;
    private SessionService _sessionService;
    private User _user;
    private UserDTO _userDto;

    [TestInitialize]
    public void SetUp()
    {
        _contextFactory = new InMemoryAppContextFactory();
        _context = _contextFactory.CreateDbContext();
        _userRepository = new UserRepository(_context);
        _userService = new UserService(_userRepository);
        _sessionService = new SessionService(_userRepository);
        _user = new User(1, "Tim", "Robbins", "timrobbins@email.com", "123456", "User");
        _userDto = new UserDTO(1, "Tim", "Robbins", "timrobbins@email.com", "123456", "User");
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Login_WhenCalledWithInvalidEmailOrPassword_ThenThrowsException()
    {
        //arrange
        //act
        _sessionService.Login(_userDto.Email, _userDto.Password);
        //assert
    }

    [TestMethod]
    public void GetLoggedUser_WhenCalledWithUserNotLoggedIn_ThenLoggedUserIsNull()
    {
        //arrange
        //act
        UserDTO loggedUser = _sessionService.GetLoggedUser();
        //assert
        Assert.IsNull(loggedUser);
    }

    [TestMethod]
    public void GetLoggedUser_WhenCalledWithUserLoggedIn_ThenLoggedUserIsNotNull()
    {
        //arrange
        _userRepository.AddUser(_user);
        _sessionService.Login(_user.Email, _user.Password);
        //act
        UserDTO loggedUser = _sessionService.GetLoggedUser();
        //assert
        Assert.IsNotNull(loggedUser);
        Assert.AreEqual(_user.Name, loggedUser.Name);
        Assert.AreEqual(_user.LastName, loggedUser.LastName);
        Assert.AreEqual(_user.Email, loggedUser.Email);
        Assert.AreEqual(_user.Role, loggedUser.Role);
    }

    [TestMethod]
    public void Logout_WhenCalledAfterSessionLogout_ThenLoggedUserIsNull()
    {
        //arrange
        _userRepository.AddUser(_user);
        _sessionService.Login(_user.Email, _user.Password);
        //act
        _sessionService.Logout();
        //assert
        Assert.IsNull(_sessionService.GetLoggedUser());
    }
}