using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace BookReviewApp.Web.Utilities
{
    /// <summary>
    /// Helper class for validating file uploads (e.g., images).
    /// </summary>
    public static class FileUploadHelper
    {
        private static readonly string[] AllowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxImageSizeBytes = 2 * 1024 * 1024; // 2MB

        /// <summary>
        /// Validates if the uploaded file is an allowed image type and within the size limit.
        /// </summary>
        /// <param name="file">The uploaded file.</param>
        /// <param name="errorMessage">The error message if validation fails.</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool IsValidImage(IFormFile file, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (file == null || file.Length == 0)
            {
                errorMessage = "No file uploaded.";
                return false;
            }
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedImageExtensions.Contains(ext))
            {
                errorMessage = "Only JPG, PNG, GIF, or WEBP images are allowed.";
                return false;
            }
            if (file.Length > MaxImageSizeBytes)
            {
                errorMessage = "Image size must be less than 2MB.";
                return false;
            }
            return true;
        }
    }
} 