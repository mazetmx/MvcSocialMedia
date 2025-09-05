using System.ComponentModel.DataAnnotations.Schema;

namespace MvcSocialMedia.Models;

public class Following
{
    public int Id { get; set; }

    // Navigation
    [ForeignKey("Follower")]
    public int FollowerId { get; set; }
    public virtual User Follower { get; set; } = null!;

    [ForeignKey("Followee")]
    public int FolloweeId { get; set; }
    public virtual User Followee { get; set; } = null!;
}