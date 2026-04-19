using DataAccess;
using Domain;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class SessionService : ISessionService
{
    private readonly InMemoryUserRepository _userRepository;
    private LoggedUserDTO _loggedUserDTO;

    public SessionService(InMemoryUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public LoggedUserDTO GetLoggedUser()
    {
        return _loggedUserDTO;
    }

    public void Login(string email, string password)
    {
        if (_loggedUserDTO != null) return;

        User? user =
            _userRepository.GetUsers().FirstOrDefault(user => user.Email == email && user.Password == password);

        _loggedUserDTO = user != null
            ? FromEntity(user)
            : throw new ArgumentException("User or password is incorrect, try again");
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