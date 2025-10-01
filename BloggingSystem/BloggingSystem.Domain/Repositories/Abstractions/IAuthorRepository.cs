using Blog.Persistence;

namespace BloggingSystem.Domain.Repositories.Abstractions
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(Guid id);
    }
}
