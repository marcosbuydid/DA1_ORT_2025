using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests;

[TestClass]
public class UserRepositoryTests
{
    private AppDbContext _context;
    private InMemoryAppContextFactory _contextFactory;
    private IUserRepository _userRepository;
    private User _user;
    private User _user_two;

    [TestInitialize]
    public void SetUp()
    {
        _contextFactory = new InMemoryAppContextFactory();
        _context = _contextFactory.CreateDbContext();
        _userRepository = new UserRepository(_context);
        _user = new User(1, "Tim", "Robbins", "timrobbins@email.com", "123456", "User");
        _user_two = new User(2, "Leonardo", "Di Caprio", "dicapriol@email.com", "654321", "User");
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void AddUser_WhenCalledWithEmptyUser_ThenThrowsException()
    {
        //arrange
        //act
        _userRepository.AddUser(new User());
    }

    [TestMethod]
    public void AddUser_WhenCalled_ThenUserIsAdded()
    {
        //arrange
        //act
        _userRepository.AddUser(_user);
        //assert
        List<User> users = _userRepository.GetUsers();
        Assert.AreEqual(1, users.Count);
        Assert.AreEqual("timrobbins@email.com", users[0].Email);
    }

    [TestMethod]
    public void GetUsers_WhenCalled_ThenUsersAreReturned()
    {
        //arrange
        _userRepository.AddUser(_user);
        _userRepository.AddUser(_user_two);
        //act
        List<User> users = _userRepository.GetUsers();
        //assert
        Assert.AreEqual(2, users.Count);
    }

    [TestMethod]
    public void GetUser_WhenCalled_ThenUserIsReturned()
    {
        //arrange
        _userRepository.AddUser(_user);
        //act
        User? result = _userRepository.GetUser(u => u.Id == 1);
        //assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Tim", result.Name);
        Assert.AreEqual("Robbins", result.LastName);
        Assert.AreEqual("timrobbins@email.com", result.Email);
        Assert.AreEqual("123456", result.Password);
        Assert.AreEqual("User", result.Role);
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void DeleteUser_WhenCalledWithUserThatDoesNotExist_ThenThrowsException()
    {
        //arrange
        User aUser = new User(991, "Unknown", "Unknown", "email", "123", "User");
        //act
        _userRepository.DeleteUser(_user);
    }

    [TestMethod]
    public void DeleteUser_WhenCalled_ThenUserIsDeleted()
    {
        //arrange
        _userRepository.AddUser(_user);
        //act
        _userRepository.DeleteUser(_user);
        //assert
        List<User> result = _userRepository.GetUsers();
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void UpdateUser_WhenCalledWithDetachedUser_ThenThrowsException()
    {
        //arrange
        //we simulate the movie is not tracked
        User aUser = new User(991, "Unknown", "Unknown", "email", "123", "User");
        //act
        //it tries to update an object that`s not in the context
        _userRepository.UpdateUser(aUser);
    }

    [TestMethod]
    public void UpdateUser_WhenCalled_ThenUserIsUpdated()
    {
        //arrange
        _userRepository.AddUser(_user);
        _user.Name = "Updated Name";
        _user.LastName = "Updated LastName";
        _user.Role = "Updated Role";
        //act
        _userRepository.UpdateUser(_user);
        //assert
        User? userUpdated = _userRepository.GetUser(u => u.Id == 1);
        Assert.AreEqual("Updated Name", userUpdated.Name);
        Assert.AreEqual("Updated LastName", userUpdated.LastName);
        Assert.AreEqual("Updated Role", userUpdated.Role);
    }
}