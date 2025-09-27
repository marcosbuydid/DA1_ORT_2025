using DataAccess.Interfaces;
using Domain;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ISecureDataService _secureDataService;

    public UserService(IUserRepository userRepository, ISecureDataService secureDataService)
    {
        _userRepository = userRepository;
        _secureDataService = secureDataService;
    }

    public void AddUser(UserDTO user)
    {
        ValidateUserEmail(user.Email);
        user.Password = _secureDataService.Hash(user.Password);
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
        User? userToDelete = _userRepository.GetUser(u => u.Email == email);
        if (userToDelete == null)
        {
            throw new ArgumentException("Cannot find a user with this email");
        }

        _userRepository.DeleteUser(userToDelete);
    }

    public void UpdateUser(UserDTO userToUpdate)
    {
        User? user = _userRepository.GetUser(u => u.Email == userToUpdate.Email);
        if (user == null)
        {
            throw new ArgumentException("Cannot find the specified user");
        }

        user.Name = userToUpdate.Name;
        user.LastName = userToUpdate.LastName;
        user.Email = userToUpdate.Email;
        user.Role = userToUpdate.Role;
        _userRepository.UpdateUser(user);
    }

    public UserDTO GetUser(string email)
    {
        User? user = _userRepository.GetUser(user => user.Email == email);
        if (user == null)
        {
            throw new ArgumentException("Cannot find a user with this email");
        }

        return FromEntity(user);
    }
    
    public void ChangePassword(ChangePasswordDTO changePasswordDTO)
    {
        User? user = ValidateOldPassword(changePasswordDTO.UserEmail, changePasswordDTO.OldPassword);
        if (user != null)
        {
            string newPasswordHash = _secureDataService.Hash(changePasswordDTO.NewPassword);
            user.Password = newPasswordHash;
            _userRepository.UpdateUser(user);
        }
        else
        {
            throw new ArgumentException("Old password entered is incorrect");
        }
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
    
    private User? ValidateOldPassword(string email, string inputPassword)
    {
        User? user = _userRepository.GetUser(user => user.Email == email);
        if (user == null)
        {
            throw new ArgumentException("Cannot find user with this email");
        }

        string storedHashedPassword = user.Password;
        bool hashesMatch = _secureDataService.CompareHashes(storedHashedPassword,inputPassword);

        return hashesMatch ? user : null;
    }

    private static User ToEntity(UserDTO userDTO)
    {
        return new User(userDTO.Id, userDTO.Name, userDTO.LastName, userDTO.Email, userDTO.Password, userDTO.Role);
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