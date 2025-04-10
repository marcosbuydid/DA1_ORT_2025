using Services.Models;

namespace Services.Interfaces;

public interface ISessionService
{
    UserDTO GetLoggedUser();
    void Login(string username, string password);
    void Logout();
}