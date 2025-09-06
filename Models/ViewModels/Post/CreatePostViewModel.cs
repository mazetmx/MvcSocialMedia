using System.ComponentModel.DataAnnotations;

namespace MvcSocialMedia.Models.ViewModels.Post;

public class CreatePostViewModel
{
    [MaxLength(500, ErrorMessage="Post cannot exceed 500 characters")]
    public required string Content { get; set; }
}