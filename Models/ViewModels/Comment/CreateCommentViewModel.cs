using System.ComponentModel.DataAnnotations;

namespace MvcSocialMedia.Models.ViewModels.Comment;

public class CreateCommentViewModel
{
    public int PostId { get; set; }

    [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
    [Display(Name = "Add a comment")]
    public required string Content { get; set; }
}