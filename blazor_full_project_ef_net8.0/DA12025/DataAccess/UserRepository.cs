using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class UserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public List<User> GetUsers()
    {
        return _appDbContext.Set<User>().AsQueryable<User>().ToList();
    }

    public User? GetUser(Func<User, bool> filter)
    {
        return _appDbContext.Set<User>().FirstOrDefault(filter);
    }

    public void AddUser(User user)
    {
        try
        {
            _appDbContext.Set<User>().Add(user);
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Cannot add the user to database", e);
        }
    }

    public void UpdateUser(User user)
    {
        try
        {
            _appDbContext.Update(user);
            _appDbContext.Entry(user).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Cannot update the user", e);
        }
    }

    public void DeleteUser(User user)
    {
        try
        {
            _appDbContext.Set<User>().Remove(user);
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Cannot delete the user", e);
        }
    }
}