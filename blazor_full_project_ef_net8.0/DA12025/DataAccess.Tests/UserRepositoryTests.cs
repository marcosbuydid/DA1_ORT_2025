using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests;

[TestClass]
public class UserRepositoryTests
{
    private AppDbContext _context;
    private InMemoryAppContextFactory _contextFactory;
    private UserRepository _userRepository;
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
    public void Add_WhenAddIsInvokedWithAnEmptyUser_ThenThrowException()
    {
        //arrange
        //act
        _userRepository.Add(new User());
    }

    [TestMethod]
    public void Add_WhenAddIsInvoked_ThenTheUserIsAdded()
    {
        //arrange
        //act
        _userRepository.Add(_user);
        //assert
        IList<User> users = _userRepository.GetAll();
        Assert.AreEqual(1, users.Count);
        Assert.AreEqual("timrobbins@email.com", users[0].Email);
    }

    [TestMethod]
    public void GetAll_WhenGetAllIsInvoked_ThenAllUsersAreReturned()
    {
        //arrange
        _userRepository.Add(_user);
        _userRepository.Add(_user_two);
        //act
        IList<User> users = _userRepository.GetAll();
        //assert
        Assert.AreEqual(2, users.Count);
    }

    [TestMethod]
    public void Get_WhenGetIsInvoked_ThenTheUserIsReturned()
    {
        //arrange
        _userRepository.Add(_user);
        //act
        User? result = _userRepository.Get(u => u.Id == 1);
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
    public void Delete_WhenDeleteIsInvokedAndUserNotExist_ThenThrowException()
    {
        //arrange
        User aUser = new User(991, "Unknown", "Unknown", "email", "123", "User");
        //act
        _userRepository.Delete(_user);
    }

    [TestMethod]
    public void Delete_WhenDeleteIsInvoked_ThenTheUserIsRemoved()
    {
        //arrange
        _userRepository.Add(_user);
        //act
        _userRepository.Delete(_user);
        //assert
        IList<User> result = _userRepository.GetAll();
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(DbUpdateException))]
    public void Update_WhenUpdateIsInvokedWithADetachedUser_ThenThrowException()
    {
        //arrange
        User aUser = new User(991, "Unknown", "Unknown", "email", "123", "User");
        //we simulate the movie is not tracked
        //act
        _userRepository.Update(aUser);
        //it tries to update an object is not in the context
    }

    [TestMethod]
    public void Update_WhenUpdateIsInvoked_ThenTheUserIsUpdated()
    {
        //arrange
        _userRepository.Add(_user);
        _user.Name = "Updated Name";
        _user.LastName = "Updated LastName";
        _user.Role = "Updated Role";
        //act
        _userRepository.Update(_user);
        //assert
        User? userUpdated = _userRepository.Get(u => u.Id == 1);
        Assert.AreEqual("Updated Name", userUpdated.Name);
        Assert.AreEqual("Updated LastName", userUpdated.LastName);
        Assert.AreEqual("Updated Role", userUpdated.Role);
    }
}