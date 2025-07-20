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
    /// Service for managing book-related operations.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        /// <summary>
        /// Gets all books with their associated authors.
        /// </summary>
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets a book by its unique identifier.
        /// </summary>
        public async Task<Book?> GetBookByIdAsync(string id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Adds a new book to the repository.
        /// </summary>
        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            return book;
        }

        /// <summary>
        /// Updates an existing book in the repository.
        /// </summary>
        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }

        /// <summary>
        /// Deletes a book by its unique identifier.
        /// </summary>
        public async Task DeleteBookAsync(string id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Gets all books with their associated authors.
        /// </summary>
        public async Task<IEnumerable<Book>> GetAllBooksWithAuthorsAsync()
        {
            return await _bookRepository.GetAllAsync(
                include: query => query.Include(b => b.Author));
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(string authorId)
        {
            return await _bookRepository.GetAllAsync(
                filter: query => query.Where(b => b.AuthorId == authorId),
                include: query => query.Include(b => b.Author));
        }
    }
}