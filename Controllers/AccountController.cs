using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSocialMedia.Models;
using MvcSocialMedia.Models.ViewModels.Account;

namespace MvcSocialMedia.Controllers;

[Authorize]
public class AccountController(UserManager<User> userManager, SignInManager<User> signInManager) : Controller
{
    // GET: Account/Register
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    // POST: Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    // GET: Account/Login
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    // POST: Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    // GET: Account/Profile
    public async Task<IActionResult> Profile()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with Id '{userManager.GetUserId(User)}'.");
        }

        var model = new ProfileViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            UserName = user.UserName!
        };

        return View(model);
    }

    // POST: /Account/Profile
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                model.UserName = user.UserName!;
                model.Email = user.Email!;
            }
            return View(model);
        }

        var userToUpdate = await userManager.GetUserAsync(User);
        if (userToUpdate == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        userToUpdate.FirstName = model.FirstName;
        userToUpdate.LastName = model.LastName;

        var result = await userManager.UpdateAsync(userToUpdate);

        if (result.Succeeded)
        {
            var updatedUser = await userManager.FindByIdAsync(userToUpdate.Id);

            await signInManager.RefreshSignInAsync(userToUpdate);

            TempData["SuccessMessage"] = "Your profile has been updated successfully!";
            return RedirectToAction(nameof(Profile));
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        model.UserName = userToUpdate.UserName!;
        model.Email = userToUpdate.Email!;

        return View(model);
    }
}