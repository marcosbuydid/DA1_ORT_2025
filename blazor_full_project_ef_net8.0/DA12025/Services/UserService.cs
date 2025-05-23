using DataAccess;
using Domain;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(UserDTO user)
    {
        ValidateUserEmail(user.Email);
        _userRepository.Add(ToEntity(user));
    }

    public void UpdateUser(UserDTO userToUpdate)
    {
        User? user = _userRepository.Get(u => u.Email == userToUpdate.Email);
        if (user == null)
        {
            throw new ArgumentException("Cannot find the specified user");
        }

        user.Name = userToUpdate.Name;
        user.LastName = userToUpdate.LastName;
        user.Email = userToUpdate.Email;
        user.Role = userToUpdate.Role;
        _userRepository.Update(user);
    }

    public List<UserDTO> GetUsers()
    {
        List<UserDTO> usersDTO = new List<UserDTO>();

        foreach (var user in _userRepository.GetAll())
        {
            usersDTO.Add(FromEntity(user));
        }

        return usersDTO;
    }

    public void DeleteUser(string email)
    {
        User? userToDelete = _userRepository.Get(u => u.Email == email);
        if (userToDelete == null)
        {
            throw new ArgumentException("Cannot find user with this email");
        }

        _userRepository.Delete(userToDelete);
    }

    public UserDTO GetUser(string email)
    {
        User? user = _userRepository.Get(user => user.Email == email);
        if (user == null)
        {
            throw new ArgumentException("Cannot find user with this email");
        }

        return FromEntity(user);
    }

    private void ValidateUserEmail(string email)
    {
        foreach (var user in _userRepository.GetAll())
        {
            if (user.Email == email)
            {
                throw new ArgumentException("There`s a user already defined with that email");
            }
        }
    }

    private User ToEntity(UserDTO userDTO)
    {
        return new User(userDTO.Id, userDTO.Name, userDTO.LastName, userDTO.Email, userDTO.Password, userDTO.Role) { };
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