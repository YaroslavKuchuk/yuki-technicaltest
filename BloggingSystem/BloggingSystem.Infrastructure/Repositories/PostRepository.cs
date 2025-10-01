using Blog.Domain;
using Blog.Infrastructure;
using BloggingSystem.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BloggingSystem.Infrastructure.Repositories
{
    public sealed class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Post details)
        {
            _context.Posts.Add(details);
            await _context.SaveChangesAsync();

            return details.Id;
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post
                = await _context
                    .Posts
                        .FirstOrDefaultAsync(p => p.Id == id);

            return post;
        }
    }
}
