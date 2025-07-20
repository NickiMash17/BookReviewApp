using BookReviewApp.Domain.Models;

namespace BookReviewApp.Services.Interfaces
{
    /// <summary>
    /// Interface for user-related service operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        Task<User?> GetUserByIdAsync(string id);

        /// <summary>
        /// Gets a user by their email address.
        /// </summary>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        Task<User?> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        Task AddUserAsync(User user);

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        Task DeleteUserAsync(string id);

        /// <summary>
        /// Checks if a user with the specified ID exists.
        /// </summary>
        Task<bool> UserExistsAsync(string id);

        /// <summary>
        /// Checks if a user with the specified email address exists.
        /// </summary>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>
        /// Checks if a user with the specified username exists.
        /// </summary>
        Task<bool> UsernameExistsAsync(string username);

        /// <summary>
        /// Validates user credentials (email and password).
        /// </summary>
        Task<bool> ValidateCredentialsAsync(string email, string password);

        /// <summary>
        /// Updates the last login timestamp for a user.
        /// </summary>
        Task UpdateLastLoginAsync(string userId);
    }
} 