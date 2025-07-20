using BookReviewApp.Domain.Models;

namespace BookReviewApp.Services.Interfaces
{
    /// <summary>
    /// Interface for author-related service operations.
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Gets all authors.
        /// </summary>
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        /// <summary>
        /// Gets an author by their unique identifier.
        /// </summary>
        Task<Author?> GetAuthorByIdAsync(string id);

        /// <summary>
        /// Adds a new author to the repository.
        /// </summary>
        Task AddAuthorAsync(Author author);

        /// <summary>
        /// Updates an existing author in the repository.
        /// </summary>
        Task UpdateAuthorAsync(Author author);

        /// <summary>
        /// Deletes an author by their unique identifier.
        /// </summary>
        Task DeleteAuthorAsync(string id);
        Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
        Task<bool> AuthorExistsAsync(string id);
    }
} 