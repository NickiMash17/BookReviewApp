using BookReviewApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetReviewsWithUserAndBookAsync();
            return View(reviews);
        }

        public async Task<IActionResult> Details(int id)
        {
            var review = (await _reviewService.GetReviewsWithUserAndBookAsync()).FirstOrDefault(r => r.ReviewId == id);
            if (review == null)
                return NotFound();
            return View(review);
        }
    }
} 