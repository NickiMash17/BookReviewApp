using Microsoft.AspNetCore.Mvc;
using BookReviewApp.Services.Interfaces;
using BookReviewApp.Web.Models;
using BookReviewApp.Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BookReviewApp.Web.Utilities;

namespace BookReviewApp.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                if (await _userService.EmailExistsAsync(model.Email))
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(model);
                }
                if (await _userService.UsernameExistsAsync(model.Username))
                {
                    ModelState.AddModelError("Username", "Username is already taken.");
                    return View(model);
                }

                var user = new User
                {
                    Email = model.Email,
                    Username = model.Username,
                    PasswordHash = model.Password, // Will be hashed in service
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailConfirmed = true, // Auto-confirm for development
                    IsActive = true
                };
                await _userService.AddUserAsync(user);
                
                // For development: auto-sign in the user
                await SignInUser(user, false);
                TempData["SuccessMessage"] = "Registration successful! You are now logged in.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred during registration.");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.GetUserByEmailAsync(model.EmailOrUsername)
                       ?? await _userService.GetUserByUsernameAsync(model.EmailOrUsername);
            if (user == null || !user.IsActive)
            {
                ModelState.AddModelError("", "Invalid credentials or inactive account.");
                return View(model);
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Please confirm your email before logging in.");
                return View(model);
            }
            var valid = await _userService.ValidateCredentialsAsync(user.Email, model.Password);
            if (!valid)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }
            // Set authentication cookie
            await SignInUser(user, model.RememberMe);
            TempData["SuccessMessage"] = $"Welcome back, {user.Username}!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return RedirectToAction("Login");
            var model = new ProfileViewModel
            {
                Email = user.Email,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
            // Fetch user's reviews
            var reviews = user.Reviews?.OrderByDescending(r => r.ReviewDate).ToList() ?? new List<BookReviewApp.Domain.Models.Review>();
            ViewBag.MyReviews = reviews;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model, IFormFile? profileImage)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                    return RedirectToAction("Login");
                // Check for email/username conflicts (if changed)
                if (user.Email != model.Email && await _userService.EmailExistsAsync(model.Email))
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(model);
                }
                if (user.Username != model.Username && await _userService.UsernameExistsAsync(model.Username))
                {
                    ModelState.AddModelError("Username", "Username is already taken.");
                    return View(model);
                }
                user.Email = model.Email;
                user.Username = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                // Handle profile image upload with validation
                if (profileImage != null && profileImage.Length > 0)
                {
                    if (!FileUploadHelper.IsValidImage(profileImage, out var errorMessage))
                    {
                        ModelState.AddModelError("", errorMessage);
                        return View(model);
                    }
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars");
                    Directory.CreateDirectory(uploadsFolder);
                    var ext = Path.GetExtension(profileImage.FileName).ToLowerInvariant();
                    var fileName = $"user_{userId}_{DateTime.Now.Ticks}{ext}";
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileImage.CopyToAsync(stream);
                    }
                    user.ProfilePictureUrl = $"/images/avatars/{fileName}";
                }
                await _userService.UpdateUserAsync(user);
                await SignInUser(user, rememberMe: false); // Refresh claims for avatar
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while updating your profile.");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                    return RedirectToAction("Login");
                var valid = await _userService.ValidateCredentialsAsync(user.Email, model.CurrentPassword);
                if (!valid)
                {
                    ModelState.AddModelError("CurrentPassword", "Current password is incorrect.");
                    return View(model);
                }
                user.PasswordHash = new Microsoft.AspNetCore.Identity.PasswordHasher<BookReviewApp.Domain.Models.User>().HashPassword(user, model.NewPassword);
                await _userService.UpdateUserAsync(user);
                TempData["SuccessMessage"] = "Password changed successfully!";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while changing your password.");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAvatar()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                    return RedirectToAction("Login");
                user.ProfilePictureUrl = null;
                await _userService.UpdateUserAsync(user);
                await SignInUser(user, rememberMe: false);
                TempData["SuccessMessage"] = "Avatar removed.";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while removing your avatar.");
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    TempData["SuccessMessage"] = "If this email exists, you will receive a reset link.";
                    return RedirectToAction("Login");
                }
                // For demo: redirect to ResetPassword with email as query param
                return RedirectToAction("ResetPassword", new { email = model.Email });
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while processing your password reset request.");
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("ForgotPassword");
            var model = new ResetPasswordViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "No user found with this email.");
                    return View(model);
                }
                // Hash and update password
                user.PasswordHash = new Microsoft.AspNetCore.Identity.PasswordHasher<BookReviewApp.Domain.Models.User>().HashPassword(user, model.NewPassword);
                await _userService.UpdateUserAsync(user);
                TempData["SuccessMessage"] = "Password reset successful! Please log in.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while resetting your password.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Invalid user.";
                    return RedirectToAction("Login");
                }
                // In a real app, validate the token
                user.EmailConfirmed = true;
                await _userService.UpdateUserAsync(user);
                TempData["SuccessMessage"] = "Email confirmed successfully! You can now log in.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while confirming your email.";
                return HandleError(ex, "An error occurred while confirming your email.");
            }
        }

        private string GenerateEmailToken(BookReviewApp.Domain.Models.User user)
        {
            // For demo: simple hash of userId + email
            using var sha256 = SHA256.Create();
            var input = $"{user.UserId}:{user.Email}";
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes).Replace("/", "_").Replace("+", "-");
        }

        private async Task SignInUser(User user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
            {
                claims.Add(new Claim("ProfilePictureUrl", user.ProfilePictureUrl));
            }
            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("Cookies", principal,
                new AuthenticationProperties { IsPersistent = rememberMe });
            await _userService.UpdateLastLoginAsync(user.UserId);
        }
    }
} 