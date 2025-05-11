using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookReviewApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            var query = _dbSet.AsQueryable();
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            
            return await query.FirstOrDefaultAsync(e => 
                EF.Property<int>(e, $"{typeof(T).Name}Id") == id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
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