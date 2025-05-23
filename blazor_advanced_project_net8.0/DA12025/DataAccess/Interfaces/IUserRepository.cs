using Domain;

namespace DataAccess.Interfaces;

public interface IUserRepository
{
    List<User> GetAllUsers();
    User? Get(Func<User, bool> filter);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
}