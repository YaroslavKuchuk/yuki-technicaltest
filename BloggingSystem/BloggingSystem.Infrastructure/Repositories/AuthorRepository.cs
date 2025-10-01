using Blog.Domain;
using Blog.Infrastructure;
using BloggingSystem.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BloggingSystem.Infrastructure.Repositories
{
    public sealed class AuthorRepository : IAuthorRepository
    {
        private readonly BlogDbContext _context;
        public AuthorRepository(BlogDbContext context) 
        {
            _context = context;
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            var author
                = await _context
                    .Authors
                        .FirstOrDefaultAsync(x => x.Id == id);

            return author;
        }
    }
}
