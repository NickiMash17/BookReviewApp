using System.Linq.Expressions;

namespace BookReviewApp.Domain.Interfaces
{
    /// <summary>
    /// Interface for generic repository operations.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entities of type T.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IQueryable<T>>? filter = null);
        /// <summary>
        /// Gets an entity by its unique identifier.
        /// </summary>
        Task<T?> GetByIdAsync(string id, Func<IQueryable<T>, IQueryable<T>>? include = null);
        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        Task AddAsync(T entity);
        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        Task UpdateAsync(T entity);
        /// <summary>
        /// Deletes an entity by its unique identifier.
        /// </summary>
        Task DeleteAsync(string id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}