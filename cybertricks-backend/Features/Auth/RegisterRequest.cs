using System.ComponentModel.DataAnnotations;

namespace ct.backend.Features.Auth;

public class RegisterRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirmed password are not the same.")]
    public string ConfirmPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} length {2} to {1} characters.", MinimumLength = 3)]
    [DataType(DataType.Text)]
    public string FirstName{ set; get; }
    public string LastName { set; get; }

    public string? ReturnUrl { set; get; }

}
