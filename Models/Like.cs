using System.ComponentModel.DataAnnotations.Schema;

namespace MvcSocialMedia.Models;

public class Like
{
    public int Id { get; set; }

    // Navigation
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    [ForeignKey("Post")]
    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;
}