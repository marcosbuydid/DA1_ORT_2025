using System.ComponentModel.DataAnnotations;

namespace Services.Models;

public class ChangePasswordDTO
{
    public string UserEmail { get; set; }

    [Required(ErrorMessage = "Old password is required.")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = " New password is required.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = " Retyped new password is required.")]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
    public string RetypedNewPassword { get; set; }

    public ChangePasswordDTO(string email, string oldPassword, string newPassword, string retypedNewPassword)
    {
        UserEmail = email;
        OldPassword = oldPassword;
        NewPassword = newPassword;
        RetypedNewPassword = retypedNewPassword;
    }
}