namespace BookReviewApp.Data
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string LocalConnectionString { get; set; } = string.Empty;
        public bool UseAtlas { get; set; } = true;
    }
} 