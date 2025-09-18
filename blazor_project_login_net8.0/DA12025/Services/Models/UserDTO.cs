using System.ComponentModel.DataAnnotations;
using Domain;

namespace Services.Models;

public class UserDTO
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "LastName is required.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public string Role { get; set; }

    public UserDTO() {}

    public UserDTO(string name, string lastName, string email, string password, string role)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
    }
}