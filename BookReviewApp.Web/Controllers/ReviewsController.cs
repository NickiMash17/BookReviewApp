using BookReviewApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Ganss.XSS;

namespace BookReviewApp.Web.Controllers
{
    [Authorize]
    public class ReviewsController : BaseController
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var reviews = await _reviewService.GetReviewsWithUserAndBookAsync();
                return View(reviews);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while loading reviews.");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var review = (await _reviewService.GetReviewsWithUserAndBookAsync()).FirstOrDefault(r => r.ReviewId == id);
                if (review == null)
                    return NotFound();
                return View(review);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while loading review details.");
            }
        }

        [HttpGet]
        public IActionResult Create(string bookId)
        {
            return View(new ReviewViewModel { BookId = bookId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var sanitizer = new HtmlSanitizer();
            var sanitizedComment = sanitizer.Sanitize(model.Comment);
            var review = new Review
            {
                Rating = model.Rating,
                Comment = sanitizedComment,
                BookId = model.BookId,
                UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value,
                ReviewDate = DateTime.UtcNow,
                IsApproved = false // For moderation
            };
            await _reviewService.AddReviewAsync(review);
            TempData["SuccessMessage"] = "Review submitted and pending approval.";
            return RedirectToAction("Details", "Books", new { id = model.BookId });
        }
    }

    public class ReviewViewModel
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        [StringLength(1000)]
        public string? Comment { get; set; }
        [Required]
        public string BookId { get; set; } = string.Empty;
    }
} 