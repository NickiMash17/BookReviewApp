using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookReviewApp.Data.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BookReviewApp;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}