using DataAccess.Interfaces;
using Domain;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class SessionService : ISessionService
{
    private readonly IUserRepository _userRepository;
    private readonly ISecureDataService _secureDataService;

    public SessionService(IUserRepository userRepository, ISecureDataService secureDataService)
    {
        _userRepository = userRepository;
        _secureDataService = secureDataService;
    }

    public UserDTO GetLoggedUser()
    {
        return LoggedUser.Current;
    }

    public void Login(string email, string password)
    {
        if (LoggedUser.Current != null) return;

        User? user = _userRepository.GetUsers()
            .FirstOrDefault(u => u.Email == email);

        if (user is null || !_secureDataService.CompareHashes(user.Password, password))
        {
            throw new ArgumentException("User or password is incorrect, try again");
        }

        LoggedUser.Current = FromEntity(user);
    }

    public void Logout()
    {
        LoggedUser.Current = null;
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