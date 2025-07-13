using BookReviewApp.Domain.Models;

namespace BookReviewApp.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(string id);
        Task<Author> AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(string id);
        Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
        Task<bool> AuthorExistsAsync(string id);
    }
} 