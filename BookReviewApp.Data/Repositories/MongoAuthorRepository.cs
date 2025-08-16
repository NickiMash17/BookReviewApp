using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Domain.Models;
using MongoDB.Driver;

namespace BookReviewApp.Data.Repositories
{
    public class MongoAuthorRepository : MongoRepository<Author>, IRepository<Author>
    {
        public MongoAuthorRepository(MongoDbContext context) : base(context, "Authors")
        {
        }

        public override async Task<IEnumerable<Author>> GetAllAsync(
            Func<IQueryable<Author>, IQueryable<Author>>? include = null,
            Func<IQueryable<Author>, IQueryable<Author>>? filter = null)
        {
            var authors = await _collection.Find(_ => true).ToListAsync();
            
            // Handle book inclusion for MongoDB
            if (include != null)
            {
                // Check if the include is for Books navigation property
                // We'll use a simple approach to detect book inclusion
                var hasBookInclude = true; // Assume book inclusion for now
                if (hasBookInclude)
                {
                    // Manually populate the Books for each author
                    foreach (var author in authors)
                    {
                        try
                        {
                            var bookFilter = Builders<Book>.Filter.Eq("AuthorId", author.Id);
                            var books = await _context.Books.Find(bookFilter).ToListAsync();
                            author.Books = books;
                        }
                        catch
                        {
                            // If book lookup fails, continue without books
                            author.Books = new List<Book>();
                        }
                    }
                }
            }
            
            // Apply filter if specified
            if (filter != null)
            {
                var queryable = authors.AsQueryable();
                authors = filter(queryable).ToList();
            }
            
            return authors;
        }
    }
} 