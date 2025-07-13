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
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(string id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            await _authorRepository.AddAsync(author);
            return author;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            await _authorRepository.UpdateAsync(author);
        }

        public async Task DeleteAuthorAsync(string id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
        {
            return await _authorRepository.GetAllAsync(
                include: query => query.Include(a => a.Books));
        }

        public async Task<bool> AuthorExistsAsync(string id)
        {
            return await _authorRepository.ExistsAsync(a => a.Id == id);
        }
    }
} 