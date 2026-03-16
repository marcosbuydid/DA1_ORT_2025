using DataAccess;
using Domain;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class UserService : IUserService
{
    private readonly InMemoryUserRepository _userRepository;

    public UserService(InMemoryUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(UserDTO user)
    {
        ValidateUserEmail(user.Email);
        _userRepository.AddUser(ToEntity(user));
    }

    public List<UserDTO> GetUsers()
    {
        List<UserDTO> usersDTO = new List<UserDTO>();

        foreach (var user in _userRepository.GetUsers())
        {
            usersDTO.Add(FromEntity(user));
        }

        return usersDTO;
    }

    public void DeleteUser(string email)
    {
        User? userToDelete = _userRepository.GetUser(email);
        if (userToDelete == null)
        {
            throw new ArgumentException("Cannot find the specified user");
        }

        _userRepository.DeleteUser(userToDelete);
    }

    public void UpdateUser(UserDTO userToUpdate)
    {
        User? user = _userRepository.GetUser(userToUpdate.Email);
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
        _userRepository.UpdateUser(user);
    }

    public UserDTO GetUser(string email)
    {
        User? user = _userRepository.GetUser(email);
        if (user == null)
        {
            throw new ArgumentException("Cannot find user with this email");
        }

        return FromEntity(user);
    }

    private void ValidateUserEmail(string email)
    {
        string inputEmail = email.Trim().ToLowerInvariant();
        foreach (var user in _userRepository.GetUsers())
        {
            string retrievedEmail = user.Email.Trim().ToLowerInvariant();
            if (retrievedEmail == inputEmail)
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