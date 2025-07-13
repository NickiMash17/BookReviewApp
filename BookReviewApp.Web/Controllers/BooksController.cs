//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using BookReviewApp.Services.Interfaces;
using BookReviewApp.Domain.Models;
using BookReviewApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BookReviewApp.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IReviewService _reviewService;

        public BooksController(
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
                var books = await _bookService.GetAllBooksWithAuthorsAsync();
                return View(books);
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred while loading books." });
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                // Get reviews for this book
                var reviews = await _reviewService.GetReviewsByBookIdAsync(id);
                var averageRating = await _reviewService.GetAverageRatingForBookAsync(id);

                ViewBag.Reviews = reviews;
                ViewBag.AverageRating = averageRating;
                
                return View(book);
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred while loading book details." });
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                ViewBag.Authors = authors;
                return View();
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred while loading the create form." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile? CoverImage)
        {
            IEnumerable<Author> authors = null;
            try
            {
                if (ModelState.IsValid)
                {
                    // Handle cover image upload
                    if (CoverImage != null && CoverImage.Length > 0)
                    {
                        var allowedTypes = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                        var ext = Path.GetExtension(CoverImage.FileName).ToLowerInvariant();
                        if (!allowedTypes.Contains(ext))
                        {
                            ModelState.AddModelError("", "Only JPG, PNG, or WEBP images are allowed.");
                            authors = await _authorService.GetAllAuthorsAsync();
                            ViewBag.Authors = authors;
                            return View(book);
                        }
                        if (CoverImage.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("", "Image size must be less than 2MB.");
                            authors = await _authorService.GetAllAuthorsAsync();
                            ViewBag.Authors = authors;
                            return View(book);
                        }
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books");
                        Directory.CreateDirectory(uploadsFolder);
                        var fileName = $"book_{DateTime.Now.Ticks}{ext}";
                        var filePath = Path.Combine(uploadsFolder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await CoverImage.CopyToAsync(stream);
                        }
                        book.CoverImageUrl = $"/images/books/{fileName}";
                    }
                    await _bookService.AddBookAsync(book);
                    TempData["SuccessMessage"] = "Book created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                authors = await _authorService.GetAllAuthorsAsync();
                ViewBag.Authors = authors;
                return View(book);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while creating the book.");
                if (authors == null)
                    authors = await _authorService.GetAllAuthorsAsync();
                ViewBag.Authors = authors;
                return View(book);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                var authors = await _authorService.GetAllAuthorsAsync();
                ViewBag.Authors = authors;
                return View(book);
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred while loading the edit form." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Book book)
        {
            try
            {
                if (id != book.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    await _bookService.UpdateBookAsync(book);
                    TempData["SuccessMessage"] = "Book updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                var authors = await _authorService.GetAllAuthorsAsync();
                ViewBag.Authors = authors;
                return View(book);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while updating the book.");
                var authors = await _authorService.GetAllAuthorsAsync();
                ViewBag.Authors = authors;
                return View(book);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                return View(book);
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred while loading the delete confirmation." });
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                TempData["SuccessMessage"] = "Book deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the book.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}