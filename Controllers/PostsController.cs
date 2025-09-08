using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSocialMedia.Data;
using MvcSocialMedia.Models;
using MvcSocialMedia.Models.ViewModels.Comment;
using MvcSocialMedia.Models.ViewModels.Post;

namespace MvcSocialMedia.Controllers;

[Authorize]
public class PostsController(MvcSocialMediaContext context, UserManager<User> userManager) : Controller
{
    // GET: Posts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Posts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePostViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.GetUserAsync(User);

        var post = new Post
        {
            Content = model.Content,
            UserId = user!.Id
        };

        context.Posts.Add(post);
        await context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    // GET: Posts/Feed
    public async Task<IActionResult> Feed()
    {
        var posts = await context.Posts
            .OrderByDescending(p => p.Id)
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .Take(20)
            .ToListAsync();

        var currentUserId = userManager.GetUserId(User);

        var postViewModels = posts.Select(p => new PostViewModel
        {
            Id = p.Id,
            Content = p.Content,
            UserName = p.User.UserName!,
            UserFirstName = p.User.FirstName,
            LikeCount = p.Likes.Count,
            CommentCount = p.Comments.Count,
            IsLikedByCurrentUser = p.Likes.Any(l => l.UserId == currentUserId),
            Comments = p.Comments
                .Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserFirstName = c.User.FirstName,
                    UserName = c.User.UserName!,
                    PostId = c.PostId
                }).ToList()
        }).ToList();

        return View(postViewModels);
    }
}