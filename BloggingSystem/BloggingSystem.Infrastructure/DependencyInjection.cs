using Blog.Persistence;
using BloggingSystem.Domain.Repositories.Abstractions;
using BloggingSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BloggingSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("SQL Server connection string is required. Set ConnectionStrings:Default.");

            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            return services;
        }

        public static IServiceCollection AddInfrastructureInMemory(this IServiceCollection services, string? dbName = null)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseInMemoryDatabase(dbName ?? "blogdb-test"));
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            return services;
        }
    }
}
