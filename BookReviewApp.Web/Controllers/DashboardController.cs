using BookReviewApp.Services.Interfaces;
using BookReviewApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookReviewApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IReviewService _reviewService;

        public DashboardController(
            IBookService bookService,
            IAuthorService authorService,
            IReviewService reviewService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Get all data
                var books = await _bookService.GetAllBooksWithAuthorsAsync();
                var authors = await _authorService.GetAllAuthorsAsync();
                var reviews = await _reviewService.GetAllReviewsAsync();

                // Calculate statistics
                var totalBooks = books.Count();
                var totalAuthors = authors.Count();
                var totalReviews = reviews.Count();
                var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

                // Get top rated books
                var topRatedBooks = books
                    .OrderByDescending(b => b.PublishedDate)
                    .Take(5)
                    .ToList();

                // Get recent reviews
                var recentReviews = reviews
                    .OrderByDescending(r => r.ReviewDate)
                    .Take(5)
                    .ToList();

                // Get popular authors (by book count)
                var popularAuthors = authors
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(5)
                    .ToList();

                // Monthly statistics (mock data for now)
                var monthlyStats = new List<object>
                {
                    new { Month = "Jan", Books = 12, Reviews = 45 },
                    new { Month = "Feb", Books = 18, Reviews = 67 },
                    new { Month = "Mar", Books = 15, Reviews = 52 },
                    new { Month = "Apr", Books = 22, Reviews = 78 },
                    new { Month = "May", Books = 25, Reviews = 89 },
                    new { Month = "Jun", Books = 30, Reviews = 95 }
                };

                ViewBag.TotalBooks = totalBooks;
                ViewBag.TotalAuthors = totalAuthors;
                ViewBag.TotalReviews = totalReviews;
                ViewBag.AverageRating = averageRating.ToString("F1");
                ViewBag.TopRatedBooks = topRatedBooks;
                ViewBag.RecentReviews = recentReviews;
                ViewBag.PopularAuthors = popularAuthors;
                ViewBag.MonthlyStats = monthlyStats;

                return View();
            }
            catch (Exception ex)
            {
                // Fallback values
                ViewBag.TotalBooks = 0;
                ViewBag.TotalAuthors = 0;
                ViewBag.TotalReviews = 0;
                ViewBag.AverageRating = "0.0";
                ViewBag.TopRatedBooks = new List<object>();
                ViewBag.RecentReviews = new List<object>();
                ViewBag.PopularAuthors = new List<object>();
                ViewBag.MonthlyStats = new List<object>();
                return HandleError(ex, "An error occurred while loading the dashboard.");
            }
        }
    }
} 