using Domain;
using Services.Interfaces;
using Services.Interfaces.Repositories;
using Services.Models;

namespace Services;

public class SessionService : ISessionService
{
    private readonly IUserRepository _userRepository;
    private readonly ISecureDataService _secureDataService;
    private LoggedUserDTO _loggedUserDTO;

    public SessionService(IUserRepository userRepository, ISecureDataService secureDataService)
    {
        _userRepository = userRepository;
        _secureDataService = secureDataService;
    }

    public LoggedUserDTO GetLoggedUser()
    {
        return _loggedUserDTO;
    }

    public void Login(string email, string password)
    {
        if (_loggedUserDTO != null) return;

        User? user = _userRepository.GetUsers()
            .FirstOrDefault(u => u.Email == email);

        if (user is null || !_secureDataService.CompareHashes(user.Password, password))
        {
            throw new ArgumentException("User or password is incorrect, try again");
        }

        _loggedUserDTO = FromEntity(user);
    }

    public void Logout()
    {
        _loggedUserDTO = null;
    }

    private static LoggedUserDTO FromEntity(User user)
    {
        return new LoggedUserDTO()
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };
    }
}