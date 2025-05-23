using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class UserRepository : IUserRepository
{
    protected readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public List<User> GetAllUsers()
    {
        return _appDbContext.Set<User>().AsQueryable<User>().ToList();
    }

    public User? Get(Func<User, bool> filter)
    {
        return _appDbContext.Set<User>().FirstOrDefault(filter);
    }

    public void Add(User user)
    {
        try
        {
            _appDbContext.Set<User>().Add(user);
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
    }

    public void Update(User user)
    {
        try
        {
            _appDbContext.Update(user);
            _appDbContext.Entry<User>(user).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
    }

    public void Delete(User user)
    {
        try
        {
            _appDbContext.Set<User>().Remove(user);
            _appDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
    }
}