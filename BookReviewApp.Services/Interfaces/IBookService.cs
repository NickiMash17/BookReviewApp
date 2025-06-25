using BookReviewApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookReviewApp.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksWithAuthorsAsync();
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId);
    }
}