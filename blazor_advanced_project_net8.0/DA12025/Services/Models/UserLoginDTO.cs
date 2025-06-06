﻿using System.ComponentModel.DataAnnotations;

namespace Services.Models;

public class UserLoginDTO
{
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    public UserLoginDTO(String email, String password)
    {
        Email = email;
        Password = password;
    }
}