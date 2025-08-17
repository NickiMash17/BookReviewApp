//
// This file and all code in this project are the original work of Nicolette Mashaba (nickimash).
// All rights reserved. Do not copy or redistribute without permission.
//
using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Domain.Models;
using BookReviewApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace BookReviewApp.Services.Implementations
{
    /// <summary>
    /// Service for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var users = await _userRepository.GetAllAsync(
                filter: query => query.Where(u => u.Email == email));
            return users.FirstOrDefault();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var users = await _userRepository.GetAllAsync(
                filter: query => query.Where(u => u.Username == username));
            return users.FirstOrDefault();
        }

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        public async Task AddUserAsync(User user)
        {
            try
            {
                // Hash the password before saving using simple SHA256
                user.PasswordHash = HashPassword(user.PasswordHash);
                await _userRepository.AddAsync(user);
                Console.WriteLine($"User {user.Username} added successfully with ID: {user.UserId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user {user.Username}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> UserExistsAsync(string id)
        {
            return await _userRepository.ExistsAsync(u => u.UserId == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.ExistsAsync(u => u.Email == email);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _userRepository.ExistsAsync(u => u.Username == username);
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null || !user.IsActive)
                return false;

            // For development: use simple SHA256 hashing
            var hashedPassword = HashPassword(password);
            return user.PasswordHash == hashedPassword;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task UpdateLastLoginAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.LastLoginDate = DateTime.Now;
                await _userRepository.UpdateAsync(user);
            }
        }
    }
} 