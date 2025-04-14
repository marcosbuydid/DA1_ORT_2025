using Services.Models;

namespace Services.Interfaces;

public interface IUserService
{
    List<UserDTO> GetUsers();
    UserDTO GetUser(string email);
    void AddUser(UserDTO user);
    void DeleteUser(string email);
    void UpdateUser(UserDTO user);
}