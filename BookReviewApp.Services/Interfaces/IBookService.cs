using BookReviewApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookReviewApp.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(string id);
        Task<Book> AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(string id);
        Task<IEnumerable<Book>> GetAllBooksWithAuthorsAsync();
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(string authorId);
    }
}