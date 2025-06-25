//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using BookReviewApp.Services.Interfaces;
using BookReviewApp.Domain.Models;
using BookReviewApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Details(int id)
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
        public async Task<IActionResult> Create(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _bookService.AddBookAsync(book);
                    TempData["SuccessMessage"] = "Book created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                var authors = await _authorService.GetAllAuthorsAsync();
                ViewBag.Authors = authors;
                return View(book);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while creating the book.");
                var authors = await _authorService.GetAllAuthorsAsync();
                ViewBag.Authors = authors;
                return View(book);
            }
        }

        public async Task<IActionResult> Edit(int id)
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
        public async Task<IActionResult> Edit(int id, Book book)
        {
            try
            {
                if (id != book.BookId)
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

        public async Task<IActionResult> Delete(int id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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