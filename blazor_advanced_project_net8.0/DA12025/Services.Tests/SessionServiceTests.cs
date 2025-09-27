using DataAccess;
using DataAccess.Interfaces;
using Domain;
using Microsoft.Extensions.Options;
using Services.Interfaces;
using Services.Models;
using Services.Settings;

namespace Services.Tests;

[TestClass]
public class SessionServiceTests
{
    private AppDbContext _context;
    private InMemoryAppContextFactory _contextFactory;
    private IUserRepository _userRepository;
    private ISessionService _sessionService;
    private ISecureDataService _secureDataService;
    private SystemSettings systemSettings;
    private IOptions<SystemSettings> options;
    private User _user;
    private UserDTO _userDto;

    [TestInitialize]
    public void SetUp()
    {
        _contextFactory = new InMemoryAppContextFactory();
        _context = _contextFactory.CreateDbContext();
        _userRepository = new UserRepository(_context);
        systemSettings = new SystemSettings();
        systemSettings.Token = "abcdefghijklmnopioBpLgpjWR2aHeotXSnsK1234567";
        options = Options.Create(systemSettings);
        _secureDataService = new SecureDataService(options);
        _sessionService = new SessionService(_userRepository, _secureDataService);
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
        _user.Password = _secureDataService.Hash(_user.Password);
        _userRepository.AddUser(_user);
        _sessionService.Login(_userDto.Email, _userDto.Password);
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
        _user.Password = _secureDataService.Hash(_user.Password);
        _userRepository.AddUser(_user);
        _sessionService.Login(_userDto.Email, _userDto.Password);
        //act
        _sessionService.Logout();
        //assert
        Assert.IsNull(_sessionService.GetLoggedUser());
    }
}