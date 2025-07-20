using BookReviewApp.Domain.Models;

namespace BookReviewApp.Services.Interfaces
{
    /// <summary>
    /// Interface for review-related service operations.
    /// </summary>
    public interface IReviewService
    {
        /// <summary>
        /// Gets all reviews.
        /// </summary>
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        /// <summary>
        /// Gets a review by its unique identifier.
        /// </summary>
        Task<Review?> GetReviewByIdAsync(string id);
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(string bookId);
        /// <summary>
        /// Adds a new review to the repository.
        /// </summary>
        Task AddReviewAsync(Review review);
        /// <summary>
        /// Updates an existing review in the repository.
        /// </summary>
        Task UpdateReviewAsync(Review review);
        /// <summary>
        /// Deletes a review by its unique identifier.
        /// </summary>
        Task DeleteReviewAsync(string id);
        Task<IEnumerable<Review>> GetReviewsWithUserAndBookAsync();
        Task<double> GetAverageRatingForBookAsync(string bookId);
        Task<bool> ReviewExistsAsync(string id);
    }
} 