using BookReviewApp.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookReviewApp.Data.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var connectionString = settings.Value.UseAtlas
                ? settings.Value.ConnectionString
                : settings.Value.LocalConnectionString;
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");
        public IMongoCollection<Author> Authors => _database.GetCollection<Author>("Authors");
        public IMongoCollection<Review> Reviews => _database.GetCollection<Review>("Reviews");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
        public IMongoCollection<BookCategory> BookCategories => _database.GetCollection<BookCategory>("BookCategories");
    }
}