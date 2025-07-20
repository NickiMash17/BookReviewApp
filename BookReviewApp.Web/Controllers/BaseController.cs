using Microsoft.AspNetCore.Mvc;
using BookReviewApp.Web.Models;

namespace BookReviewApp.Web.Controllers
{
    /// <summary>
    /// Base controller providing common error handling functionality.
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Handles exceptions and returns a standard error view.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="message">A user-friendly error message.</param>
        /// <returns>Error view result.</returns>
        protected IActionResult HandleError(Exception ex, string message)
        {
            // Optionally log the exception here
            var errorModel = new ErrorViewModel { RequestId = message };
            return View("Error", errorModel);
        }
    }
} 