//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using BookReviewApp.Services.Interfaces;
using BookReviewApp.Domain.Models;
using BookReviewApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookReviewApp.Web.Controllers
{
    [Authorize]
    public class AuthorsController : BaseController
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public AuthorsController(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return View(authors);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while loading authors.");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                // Get books by this author
                var books = await _bookService.GetBooksByAuthorIdAsync(id);
                ViewBag.Books = books;

                return View(author);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while loading author details.");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _authorService.AddAuthorAsync(author);
                    TempData["SuccessMessage"] = "Author created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                return View(author);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the author.");
                return HandleError(ex, "An error occurred while creating the author.");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                return View(author);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while loading the edit form.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Author author)
        {
            try
            {
                if (id != author.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    await _authorService.UpdateAuthorAsync(author);
                    TempData["SuccessMessage"] = "Author updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                return View(author);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the author.");
                return HandleError(ex, "An error occurred while updating the author.");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                // Check if author has books
                var books = await _bookService.GetBooksByAuthorIdAsync(id);
                ViewBag.Books = books;

                return View(author);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "An error occurred while loading the delete confirmation.");
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
                TempData["SuccessMessage"] = "Author deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the author.";
                return HandleError(ex, "An error occurred while deleting the author.");
            }
        }
    }
} 