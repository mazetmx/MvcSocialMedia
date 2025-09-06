namespace MvcSocialMedia.Models.ViewModels.Post;

public class PostViewModel
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string UserFirstName { get; set; } = null!;
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
}