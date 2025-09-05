using System.ComponentModel.DataAnnotations.Schema;

namespace MvcSocialMedia.Models;

public class Following
{
    public int Id { get; set; }

    // Navigation
    [ForeignKey("Follower")]
    public required string FollowerId { get; set; }
    public virtual User Follower { get; set; } = null!;

    [ForeignKey("Followee")]
    public required string FolloweeId { get; set; }
    public virtual User Followee { get; set; } = null!;
}