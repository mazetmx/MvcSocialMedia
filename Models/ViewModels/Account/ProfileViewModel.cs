using System.ComponentModel.DataAnnotations;

namespace MvcSocialMedia.Models.ViewModels.Account;

public class ProfileViewModel
{
    [Required]
    [Display(Name = "First Name")]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [MaxLength(100)]
    public string LastName { get; set; }

    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    public string UserName { get; set; }
    public DateTime MemberSince { get; set; }
}
