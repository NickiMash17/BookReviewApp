using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;

namespace BookReviewApp.Web.Utilities
{
    public static class ImageGenerator
    {
        [SupportedOSPlatform("windows")]
        public static void CreatePlaceholderImages(string wwwrootPath)
        {
            var paths = new[]
            {
                Path.Combine(wwwrootPath, "images"),
                Path.Combine(wwwrootPath, "images", "books"),
                Path.Combine(wwwrootPath, "images", "authors")
            };

            // Create directories
            foreach (var path in paths)
            {
                Directory.CreateDirectory(path);
            }

            // Create placeholder image
            var placeholderPath = Path.Combine(paths[0], "placeholder.jpg");
            CreateImage(placeholderPath, "No Image", 200, 300);

            // Create book covers
            var bookCovers = new[] { "1984", "animal-farm", "hobbit", "fellowship" };
            foreach (var book in bookCovers)
            {
                CreateImage(Path.Combine(paths[1], $"{book}.jpg"), book, 200, 300);
            }

            // Create author photos
            var authors = new[] { "orwell", "tolkien" };
            foreach (var author in authors)
            {
                CreateImage(Path.Combine(paths[2], $"{author}.jpg"), author, 150, 150);
            }
        }

        [SupportedOSPlatform("windows")]
        private static void CreateImage(string path, string text, int width, int height)
        {
            using var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);

            // Simple solid background
            graphics.Clear(Color.LightGray);

            // Draw text
            using var font = new Font("Arial", 16, FontStyle.Bold);
            var size = graphics.MeasureString(text, font);
            var x = (width - size.Width) / 2;
            var y = (height - size.Height) / 2;
            
            graphics.DrawString(text, font, Brushes.Black, x, y);
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}