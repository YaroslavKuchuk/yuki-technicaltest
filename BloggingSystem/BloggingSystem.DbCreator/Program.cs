using Blog.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
Console.WriteLine($"Environment: {env}");

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
    .Build();

var conn = config.GetConnectionString("Default")
           ?? config["ConnectionString"]
           ?? throw new InvalidOperationException("Connection string not found. Set ConnectionStrings:Default in appsettings.json.");

Console.WriteLine($"Using connection: {conn}");

var options = new DbContextOptionsBuilder<BlogDbContext>()
    .UseSqlServer(conn, sql => sql.EnableRetryOnFailure())
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
