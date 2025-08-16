//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Domain.Models;
using BookReviewApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApp.Services.Implementations
{
    /// <summary>
    /// Service for managing review-related operations.
    /// </summary>
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepository;

        public ReviewService(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        }

        /// <summary>
        /// Gets all reviews.
        /// </summary>
        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets a review by its unique identifier.
        /// </summary>
        public async Task<Review?> GetReviewByIdAsync(string id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(string bookId)
        {
            return await _reviewRepository.GetAllAsync(
                include: query => query
                    .Include(r => r.User)
                    .Include(r => r.Book),
                filter: query => query.Where(r => r.BookId == bookId && r.IsApproved));
        }

        /// <summary>
        /// Adds a new review to the repository.
        /// </summary>
        public async Task AddReviewAsync(Review review)
        {
            review.ReviewDate = DateTime.Now;
            await _reviewRepository.AddAsync(review);
        }

        /// <summary>
        /// Updates an existing review in the repository.
        /// </summary>
        public async Task UpdateReviewAsync(Review review)
        {
            await _reviewRepository.UpdateAsync(review);
        }

        /// <summary>
        /// Deletes a review by its unique identifier.
        /// </summary>
        public async Task DeleteReviewAsync(string id)
        {
            await _reviewRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsWithUserAndBookAsync()
        {
            return await _reviewRepository.GetAllAsync(
                include: query => query
                    .Include(r => r.User)
                    .Include(r => r.Book)
                    .Include(r => r.Book!.Author));
        }

        public async Task<double> GetAverageRatingForBookAsync(string bookId)
        {
            var reviews = await _reviewRepository.GetAllAsync(
                filter: query => query.Where(r => r.BookId == bookId && r.IsApproved));
            
            if (!reviews.Any())
                return 0;
                
            return reviews.Average(r => r.Rating);
        }

        public async Task<bool> ReviewExistsAsync(string id)
        {
            return await _reviewRepository.ExistsAsync(r => r.ReviewId == id);
        }
    }
} 