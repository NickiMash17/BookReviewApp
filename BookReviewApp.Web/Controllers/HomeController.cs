//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookReviewApp.Services.Interfaces;
using BookReviewApp.Domain.Models;
using BookReviewApp.Web.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.DependencyInjection;
using BookReviewApp.Data.Context;

namespace BookReviewApp.Web.Controllers;

public class HomeController : BaseController
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly IReviewService _reviewService;

    public HomeController(
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
            // Get statistics
            var books = (await _bookService.GetAllBooksWithAuthorsAsync()).ToList();
            var authors = (await _authorService.GetAllAuthorsAsync()).ToList();
            var reviews = (await _reviewService.GetAllReviewsAsync()).ToList();

            ViewBag.TotalBooks = books.Count;
            ViewBag.TotalAuthors = authors.Count;
            ViewBag.TotalReviews = reviews.Count;
            ViewBag.AverageRating = reviews.Any() ? reviews.Average(r => r.Rating).ToString("F1") : "0.0";

            // Get featured books (top 4 by rating or most recent)
            var featuredBooks = books
                .OrderByDescending(b => b.PublishedDate)
                .Take(4)
                .ToList();

            ViewBag.FeaturedBooks = featuredBooks;

            return View();
        }
        catch (Exception ex)
        {
            // Fallback values if there's an error
            ViewBag.TotalBooks = 0;
            ViewBag.TotalAuthors = 0;
            ViewBag.TotalReviews = 0;
            ViewBag.AverageRating = "0.0";
            ViewBag.FeaturedBooks = new List<object>();
            return HandleError(ex, "An error occurred while loading the home page.");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Test()
    {
        return Content("App is working! Database status: " + 
                     (HttpContext.RequestServices.GetService<ApplicationDbContext>() != null ? "Available" : "Not Available"));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
