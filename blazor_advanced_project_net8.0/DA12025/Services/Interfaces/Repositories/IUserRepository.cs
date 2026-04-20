using Domain;

namespace Services.Interfaces.Repositories;

public interface IUserRepository
{
    List<User> GetUsers();
    User? GetUser(Func<User, bool> filter);
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);
}