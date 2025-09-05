using System.ComponentModel.DataAnnotations.Schema;

namespace MvcSocialMedia.Models;

public class Comment
{
    public int Id { get; set; }

    public required string Content { get; set; }

    // Navigation
    [ForeignKey("Post")]
    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    [ForeignKey("User")]
    public required string UserId { get; set; }
    public virtual User User { get; set; } = null!;
}