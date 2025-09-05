using System.ComponentModel.DataAnnotations;

namespace MvcSocialMedia.Models.ViewModels.Account;

public class RegisterViewModel
{
    [EmailAddress]
    [Display(Name = "Email")]
    public required string Email { get; set; }

    [StringLength(100, ErrorMessage = "The password must be at least 6 and at max 100 characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }

    [Display(Name = "First Name")]
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [MaxLength(100)]
    public required string LastName { get; set; }
}