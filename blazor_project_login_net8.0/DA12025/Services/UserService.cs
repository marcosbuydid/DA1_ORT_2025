using DataAccess;
using Domain;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class UserService : IUserService
{
    private readonly InMemoryDatabase _inMemoryDatabase;

    public UserService(InMemoryDatabase inMemoryDatabase)
    {
        _inMemoryDatabase = inMemoryDatabase;
    }

    public void AddUser(UserDTO user)
    {
        ValidateUserEmail(user.Email);
        _inMemoryDatabase.AddUser(ToEntity(user));
    }

    public List<UserDTO> GetUsers()
    {
        List<UserDTO> usersDTO = new List<UserDTO>();

        foreach (var user in _inMemoryDatabase.GetUsers())
        {
            usersDTO.Add(FromEntity(user));
        }

        return usersDTO;
    }

    public void DeleteUser(string email)
    {
        User? userToDelete = _inMemoryDatabase.GetUser(email);
        if (userToDelete == null)
        {
            throw new ArgumentException("Cannot find the specified user");
        }

        _inMemoryDatabase.DeleteUser(userToDelete);
    }

    public void UpdateUser(UserDTO userToUpdate)
    {
        User? user = _inMemoryDatabase.GetUser(userToUpdate.Email);
        if (user == null)
        {
            throw new ArgumentException("Cannot find the specified user");
        }

        user.Name = userToUpdate.Name;
        user.LastName = userToUpdate.LastName;
        user.Email = userToUpdate.Email;
        //in this example password is non-updatable
        userToUpdate.Password = user.Password;
        user.Role = userToUpdate.Role;
        _inMemoryDatabase.UpdateUser(user);
    }

    public UserDTO GetUser(string email)
    {
        User? user = _inMemoryDatabase.GetUser(email);
        if (user == null)
        {
            throw new ArgumentException("Cannot find user with this email");
        }

        return FromEntity(user);
    }

    private void ValidateUserEmail(string email)
    {
        foreach (var user in _inMemoryDatabase.GetUsers())
        {
            if (user.Email == email)
            {
                throw new ArgumentException("There`s a user already defined with that email");
            }
        }
    }

    private static User ToEntity(UserDTO userDTO)
    {
        return new User(userDTO.Name, userDTO.LastName, userDTO.Email, userDTO.Password, userDTO.Role);
    }

    private static UserDTO FromEntity(User user)
    {
        return new UserDTO()
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };
    }
}