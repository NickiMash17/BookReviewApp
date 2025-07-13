using BookReviewApp.Domain.Models;

namespace BookReviewApp.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review?> GetReviewByIdAsync(string id);
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(string bookId);
        Task<Review> AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(string id);
        Task<IEnumerable<Review>> GetReviewsWithUserAndBookAsync();
        Task<double> GetAverageRatingForBookAsync(string bookId);
        Task<bool> ReviewExistsAsync(string id);
    }
} 