using Blog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure;

public sealed class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.HasDefaultSchema("blog");

        b.Entity<Author>(e =>
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

        b.Entity<Post>(e =>
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
    }
}