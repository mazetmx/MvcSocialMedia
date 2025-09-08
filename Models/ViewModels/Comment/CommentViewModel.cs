namespace MvcSocialMedia.Models.ViewModels.Comment;

public class CommentViewModel
{
    public int Id { get; set; }

    public required string Content { get; set;}

    public required string UserFirstName { get; set; }

    public required string UserName { get; set; }

    public int PostId { get; set; }
}