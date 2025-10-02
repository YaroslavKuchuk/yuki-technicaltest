using Blog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "dev";
Console.WriteLine($"Environment: {env}");

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
    .Build();

var connectionString = "Server=(local);Database=BlogDb;User Id=blogdb;Password=blogdb_123;TrustServerCertificate=True;";

Console.WriteLine($"Using connection: {connectionString}");

var options = new DbContextOptionsBuilder<BlogDbContext>()
    .UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure())
    .Options;

using var db = new BlogDbContext(options);

var pending = await db.Database.GetPendingMigrationsAsync();
if (pending.Any())
{
    Console.WriteLine($"Applying {pending.Count()} migration(s)...");
    await db.Database.MigrateAsync();
    Console.WriteLine("Migrations applied.");
}
else
{
    Console.WriteLine("No migrations found. Ensuring database is created...");
    await db.Database.EnsureCreatedAsync();
    Console.WriteLine("Database ensured.");
}

Console.WriteLine("Done.");
