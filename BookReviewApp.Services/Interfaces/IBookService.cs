using BookReviewApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookReviewApp.Services.Interfaces
{
    /// <summary>
    /// Interface for book-related service operations.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Gets all books with their associated authors.
        /// </summary>
        Task<IEnumerable<Book>> GetAllBooksWithAuthorsAsync();

        /// <summary>
        /// Gets a book by its unique identifier.
        /// </summary>
        Task<Book?> GetBookByIdAsync(string id);

        /// <summary>
        /// Adds a new book to the repository.
        /// </summary>
        Task AddBookAsync(Book book);

        /// <summary>
        /// Updates an existing book in the repository.
        /// </summary>
        Task UpdateBookAsync(Book book);

        /// <summary>
        /// Deletes a book by its unique identifier.
        /// </summary>
        Task DeleteBookAsync(string id);

        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(string authorId);
    }
}