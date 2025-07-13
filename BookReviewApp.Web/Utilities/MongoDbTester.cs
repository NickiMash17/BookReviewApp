using BookReviewApp.Data;
using BookReviewApp.Data.Context;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookReviewApp.Web.Utilities
{
    public static class MongoDbTester
    {
        public static async Task<string> TestConnectionAsync(MongoDbSettings settings)
        {
            try
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);
                
                // Test the connection by listing collections
                var collections = await database.ListCollectionNamesAsync();
                await collections.ToListAsync();
                
                return $"✅ MongoDB Connected Successfully!\n" +
                       $"Database: {settings.DatabaseName}\n" +
                       $"Connection: {settings.ConnectionString}\n" +
                       $"Collections: {collections.ToListAsync().Result.Count}";
            }
            catch (Exception ex)
            {
                return $"❌ MongoDB Connection Failed!\n" +
                       $"Error: {ex.Message}\n" +
                       $"Connection: {settings.ConnectionString}";
            }
        }

        public static async Task<string> TestLocalConnectionAsync()
        {
            var settings = new MongoDbSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "BookReviewApp"
            };
            
            return await TestConnectionAsync(settings);
        }

        public static async Task<string> TestAtlasConnectionAsync(string connectionString)
        {
            var settings = new MongoDbSettings
            {
                ConnectionString = connectionString,
                DatabaseName = "BookReviewApp"
            };
            
            return await TestConnectionAsync(settings);
        }
    }
} 