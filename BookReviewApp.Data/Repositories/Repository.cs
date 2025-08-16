using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookReviewApp.Data.Repositories
{
    /// <summary>
    /// Generic repository for CRUD operations on entities.
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Gets an entity by its unique identifier.
        /// </summary>
        public virtual async Task<T?> GetByIdAsync(string id, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            
            // Try to parse as int for backward compatibility
            if (int.TryParse(id, out int intId))
            {
                return await query.FirstOrDefaultAsync(e => 
                    EF.Property<int>(e, $"{typeof(T).Name}Id") == intId);
            }
            
            // Handle string IDs (including MongoDB ObjectIds)
            return await query.FirstOrDefaultAsync(e => 
                EF.Property<string>(e, "Id") == id);
        }

        /// <summary>
        /// Gets all entities of type T.
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IQueryable<T>>? filter = null)
        {
            try
            {
                var query = _dbSet.AsQueryable();
                
                if (filter != null)
                {
                    query = filter(query);
                }
                
                if (include != null)
                {
                    query = include(query);
                }
                
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the error and return empty collection
                Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
                return new List<T>();
            }
        }

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity by its unique identifier.
        /// </summary>
        public virtual async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}