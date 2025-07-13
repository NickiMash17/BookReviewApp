using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Domain.Models;

namespace BookReviewApp.Data.Repositories
{
    public class MongoAuthorRepository : MongoRepository<Author>, IRepository<Author>
    {
        public MongoAuthorRepository(MongoDbContext context) : base(context, "Authors")
        {
        }
    }
} 