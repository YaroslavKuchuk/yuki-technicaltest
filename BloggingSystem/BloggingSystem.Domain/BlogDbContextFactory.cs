using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog.Persistence;

public sealed class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
{
    public BlogDbContext CreateDbContext(string[] args)
    {
        var connectionString 
            = "Server=(local);Database=BlogDb;User Id=blogdb;Password=blogdb_123;TrustServerCertificate=True;";

        // Temporary solution, should be env variable in pipeline/ in local json config

        var builder = new DbContextOptionsBuilder<BlogDbContext>()
            .UseSqlServer(connectionString);

        return new BlogDbContext(builder.Options);
    }
}