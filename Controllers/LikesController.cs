using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSocialMedia.Data;
using MvcSocialMedia.Models;

namespace MvcSocialMedia.Controllers;

[Authorize]
public class LikesController(MvcSocialMediaContext context, UserManager<User> userManager) : Controller
{
    // POST: Likes/ToggleLike
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleLike(int postId)
    {
        var user = await userManager.GetUserAsync(User);
        var existingLike = await context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == user!.Id);

        if (existingLike != null)
        {
            context.Likes.Remove(existingLike);
        }
        else
        {
            var like = new Like
            {
                PostId = postId,
                UserId = user!.Id
            };
            context.Likes.Add(like);
        }

        await context.SaveChangesAsync();

        return RedirectToAction("Feed", "Posts");
    }
}