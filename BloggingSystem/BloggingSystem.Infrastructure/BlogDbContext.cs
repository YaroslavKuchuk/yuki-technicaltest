using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Blog.Infrastructure;

public sealed class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");

        modelBuilder.Entity<Author>(e =>
        {
            e.ToTable("Authors");
            e.HasKey(a => a.Id);
            e.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
            e.Property(a => a.Surname)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Post>(e =>
        {
            e.ToTable("Posts");
            e.HasKey(p => p.Id);
            e.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
            e.Property(p => p.Description)
                .HasMaxLength(500);
            e.Property(p => p.Content)
                .IsRequired();

            e.HasOne(p => p.Author)
             .WithMany(a => a.Posts)
             .HasForeignKey(p => p.AuthorId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        var author1Id = Guid.Parse("7B76DCD3-3E94-433E-8D55-74AFEAC4C33F");
        var author2Id = Guid.Parse("3A7D61B3-54DF-44B0-9516-76282354997C");

        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                Id = author1Id,
                Name = "Dileep",
                Surname = "Sreepathi"
            },
            new Author
            {
                Id = author2Id,
                Name = "Michael",
                Surname = "Maurice"
            }
        );

        var post1Id = Guid.Parse("71D6D4EA-1B4C-47A0-9BE5-6672F3DD62F0");
        var post2Id = Guid.Parse("414149FF-F2EF-4979-8E33-24335DDA74CE");

        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id = post2Id,
                AuthorId = author2Id,
                Title = "How to Use the Domain Event Pattern | DDD, Clean Architecture, .NET 9",
                Description = "description",
                Content = "content"
            },
            new Post
            {
                Id = post1Id,
                AuthorId = author1Id,
                Title = "Senior .NET Dev Interview Q/A",
                Description = "Garbage collector",
                Content = "content2"
            }
        );
    }
}