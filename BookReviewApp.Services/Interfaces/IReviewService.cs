using BookReviewApp.Domain.Models;

namespace BookReviewApp.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review?> GetReviewByIdAsync(int id);
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId);
        Task<Review> AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
        Task<IEnumerable<Review>> GetReviewsWithUserAndBookAsync();
        Task<double> GetAverageRatingForBookAsync(int bookId);
        Task<bool> ReviewExistsAsync(int id);
    }
} 