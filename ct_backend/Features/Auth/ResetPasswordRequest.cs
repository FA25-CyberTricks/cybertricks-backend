using System.ComponentModel.DataAnnotations;

namespace ct_backend.Features.Accounts;

public class ResetPasswordRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;
    [Required]
    [StringLength(100, ErrorMessage = "{0} length {2} to {1} characters.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirmed password are not the same.")]
    public string ConfirmPassword { get; set; }
}
