using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialMedia.Data;
using MvcSocialMedia.Models;

namespace MvcSocialMedia.Controllers;

[Authorize]
public class CommentsController(MvcSocialMediaContext context, UserManager<User> userManager) : Controller
{
    // POST: Comments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int postId, string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            var user = await userManager.GetUserAsync(User);

            var comment = new Comment
            {
                Content = content,
                PostId  = postId,
                UserId = user!.Id
            };

            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }

        return RedirectToAction("Feed", "Posts");
    }
}