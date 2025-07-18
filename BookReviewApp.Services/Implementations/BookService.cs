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
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookByIdAsync(string id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(string id)
        {
            await _bookRepository.DeleteAsync(id);
        }

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