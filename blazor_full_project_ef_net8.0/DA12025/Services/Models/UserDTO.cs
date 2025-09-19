using System.ComponentModel.DataAnnotations;

namespace Services.Models;

public class UserDTO
{
    public int? Id { get; set; }

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
    
    public UserDTO(){}

    public UserDTO(int? id, string name, string lastName, string email, string password, string role)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
    }
}