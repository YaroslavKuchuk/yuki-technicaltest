using Blog.Domain;

namespace BloggingSystem.Domain.Repositories.Abstractions
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(Guid id);

        Task<Guid> Add(Post details);
    }
}
