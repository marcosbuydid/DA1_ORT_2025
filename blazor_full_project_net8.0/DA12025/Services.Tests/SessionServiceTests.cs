using DataAccess;
using Domain;
using Services.Models;

namespace Services.Tests;

[TestClass]
public class SessionServiceTests
{
    private InMemoryDatabase _inMemoryDatabase;
    private SessionService _sessionService;
    private User user;
    private UserLoginDTO userLoginDTO;
    
    [TestInitialize]
    public void Setup()
    {
        _inMemoryDatabase = new InMemoryDatabase();
        _sessionService = new SessionService(_inMemoryDatabase);
        user = new User("Nick", "Williams", "nickwilliams@email.com", "p@assword", "User");
        userLoginDTO = new UserLoginDTO("nick@email.com", "password");
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Login_WhenLoginWithWrongCredentials_ThenThrowException()
    {
        //arrange
        //act
        _sessionService.Login(userLoginDTO.Email, userLoginDTO.Password);
        //assert
    }
    
    [TestMethod]
    public void LoggedUser_WhenGettingLoggedUserWithoutLogin_ThenLoggedUserIsNull()
    {
        //arrange
        //act
        UserDTO loggedUser = _sessionService.GetLoggedUser();
        //assert
        Assert.IsNull(loggedUser);
    }
    
    [TestMethod]
    public void LoggedUser_WhenGettingLoggedUserAfterLoginOk_ThenLoggedUserIsNotNull()
    {
        //arrange
        _inMemoryDatabase.Users.Clear();
        _inMemoryDatabase.Users.Add(user);
        _sessionService.Login(user.Email, user.Password);
        //act
        UserDTO loggedUser = _sessionService.GetLoggedUser();
        //assert
        Assert.IsNotNull(loggedUser);
        Assert.AreEqual(user.Name, loggedUser.Name);
        Assert.AreEqual(user.LastName, loggedUser.LastName);
        Assert.AreEqual(user.Email, loggedUser.Email);
        Assert.AreEqual(user.Role, loggedUser.Role);
    }
    
    [TestMethod]
    public void LoggedUser_WhenGettingLoggedUserAfterSessionLogout_ThenLoggedUserIsNull()
    {
        //arrange
        _inMemoryDatabase.Users.Clear();
        _inMemoryDatabase.Users.Add(user);
        _sessionService.Login(user.Email, user.Password);
        //act
        _sessionService.Logout();
        //assert
        Assert.IsNull(_sessionService.GetLoggedUser());
    }
}