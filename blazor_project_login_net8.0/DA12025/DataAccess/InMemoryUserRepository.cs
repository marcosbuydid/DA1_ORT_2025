using Domain;

namespace DataAccess;

public class InMemoryUserRepository
{
    private List<User> Users { get; }

    public InMemoryUserRepository()
    {
        Users = new List<User>();
        LoadDefaultAdministratorUser();
    }

    public List<User> GetUsers()
    {
        return Users;
    }

    public User? GetUser(string email)
    {
        User? user = Users.FirstOrDefault(user => user.Email == email);
        return user;
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void DeleteUser(User user)
    {
        Users.Remove(user);
    }

    public void UpdateUser(User userToUpdate)
    {
        User? user = Users.Find(u => u.Email == userToUpdate.Email);
        var userToUpdateIndex = Users.IndexOf(user);
        Users[userToUpdateIndex] = userToUpdate;
    }

    private void LoadDefaultAdministratorUser()
    {
        Users.Add(new User("Marcos", "Buydid", "marcosb@email.com", "123456", "Administrator"));
    }
}