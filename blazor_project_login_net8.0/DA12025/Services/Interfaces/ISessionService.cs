using Services.Models;

namespace Services.Interfaces;

public interface ISessionService
{
    LoggedUserDTO GetLoggedUser();
    void Login(string username, string password);
    void Logout();
}