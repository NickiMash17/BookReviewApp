using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace BookReviewApp.Data.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        protected readonly MongoDbContext _context;
        protected readonly IMongoCollection<T> _collection;

        public MongoRepository(MongoDbContext context, string collectionName)
        {
            _context = context;
            _collection = context.GetType()
                .GetProperty(collectionName)
                ?.GetValue(context) as IMongoCollection<T> 
                ?? throw new ArgumentException($"Collection {collectionName} not found");
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IQueryable<T>>? filter = null)
        {
            var documents = await _collection.Find(_ => true).ToListAsync();
            var queryable = documents.AsQueryable();
            
            if (filter != null)
            {
                queryable = filter(queryable);
            }
            
            if (include != null)
            {
                queryable = include(queryable);
            }
            
            return queryable.ToList();
        }

        public virtual async Task<T?> GetByIdAsync(string id, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            var document = await _collection.Find(filter).FirstOrDefaultAsync();
            
            if (document != null && include != null)
            {
                var queryable = new List<T> { document }.AsQueryable();
                queryable = include(queryable);
                return queryable.FirstOrDefault();
            }
            
            return document;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", GetIdValue(entity));
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            await _collection.DeleteOneAsync(filter);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            // For MongoDB, we'll use a simple approach
            var documents = await _collection.Find(_ => true).ToListAsync();
            return documents.AsQueryable().Any(predicate);
        }

        private string GetIdValue(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty("BookId") ?? typeof(T).GetProperty("AuthorId");
            return idProperty?.GetValue(entity)?.ToString() ?? string.Empty;
        }
    }
} 