using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Domain.Models;
using MongoDB.Driver;

namespace BookReviewApp.Data.Repositories
{
    public class MongoBookRepository : MongoRepository<Book>, IRepository<Book>
    {
        public MongoBookRepository(MongoDbContext context) : base(context, "Books")
        {
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorAsync()
        {
            var books = await _collection.Find(_ => true).ToListAsync();
            
            // For MongoDB, we'll need to manually populate the Author
            // In a real scenario, you might want to use aggregation or separate queries
            foreach (var book in books)
            {
                if (!string.IsNullOrEmpty(book.AuthorId))
                {
                    var authorFilter = Builders<Author>.Filter.Eq("_id", book.AuthorId);
                    var author = await _context.Authors.Find(authorFilter).FirstOrDefaultAsync();
                    book.Author = author;
                }
            }
            
            return books;
        }

        public async Task<Book?> GetBookWithAuthorAsync(string id)
        {
            var book = await GetByIdAsync(id);
            if (book != null && !string.IsNullOrEmpty(book.AuthorId))
            {
                var authorFilter = Builders<Author>.Filter.Eq("_id", book.AuthorId);
                var author = await _context.Authors.Find(authorFilter).FirstOrDefaultAsync();
                book.Author = author;
            }
            return book;
        }
    }
} 