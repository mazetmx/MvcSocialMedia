using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MvcSocialMedia.Models;

public class User : IdentityUser
{
    [StringLength(50)]
    public required string FirstName { get; set; } = string.Empty;

    [StringLength(50)]
    public required string LastName { get; set; } = string.Empty;

    // Navigation
    public virtual ICollection<Post> Posts { get; set; } = [];
    public virtual ICollection<Comment> Comments { get; set; } = [];
    public virtual ICollection<Like> Likes { get; set; } = [];

    public virtual ICollection<Following> Followers { get; set; } = [];
    public virtual ICollection<Following> Followees { get; set; } = [];

    public string DisplayName => $"{FirstName} {LastName}";
}