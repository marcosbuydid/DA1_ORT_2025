using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class SessionServiceTests
{
    private InMemoryDatabase _inMemoryDatabase;
    private SessionService _sessionService;
    private User _user;
    private UserLoginDTO _userLoginDTO;

    [TestInitialize]
    public void Setup()
    {
        _inMemoryDatabase = new InMemoryDatabase();
        _sessionService = new SessionService(_inMemoryDatabase);
        _user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        _userLoginDTO = new UserLoginDTO("nick@email.com", "password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Login_WhenLoginWithWrongCredentials_ThenThrowException()
    {
        //arrange
        //act
        _sessionService.Login(_userLoginDTO.Email, _userLoginDTO.Password);
        //assert
    }

    [TestMethod]
    public void GetLoggedUser_WhenGetLoggedUserWithoutLogin_ThenLoggedUserIsNull()
    {
        //arrange
        //act
        UserDTO loggedUser = _sessionService.GetLoggedUser();
        //assert
        Assert.IsNull(loggedUser);
    }

    [TestMethod]
    public void GetLoggedUser_WhenGetLoggedUserAfterLoginOk_ThenLoggedUserIsNotNull()
    {
        //arrange
        _inMemoryDatabase.GetUsers().Clear();
        _inMemoryDatabase.AddUser(_user);
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
    public void Logout_WhenGetLoggedUserAfterSessionLogout_ThenLoggedUserIsNull()
    {
        //arrange
        _inMemoryDatabase.GetUsers().Clear();
        _inMemoryDatabase.AddUser(_user);
        _sessionService.Login(_user.Email, _user.Password);
        //act
        _sessionService.Logout();
        //assert
        Assert.IsNull(_sessionService.GetLoggedUser());
    }
}