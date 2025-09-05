using System.ComponentModel.DataAnnotations.Schema;

namespace MvcSocialMedia.Models;

public class Post
{
    public int Id { get; set; }

    public required string Content { get; set; }

    // Navigation
    [ForeignKey("User")]
    public required string UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public virtual ICollection<Like> Likes { get; set; } = [];

    public virtual ICollection<Comment> Comments { get; set; } = [];
}