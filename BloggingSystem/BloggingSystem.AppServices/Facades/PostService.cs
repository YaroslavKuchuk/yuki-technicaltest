using BloggingSystem.Domain.Repositories.Abstractions;
using BloggingSystem.Presentation.Mapping;
using BloggingSystem.Presentation.ViewModels;

namespace BloggingSystem.AppServices.Facades
{
    public interface IPostService
    {
        Task<Guid> AddPostAsync(PostViewModel postViewModel);

        Task<PostViewModel> GetByIdAsync(Guid id);
    }
    public sealed class PostService : IPostService
    {
        public readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository) 
        {
            _postRepository = postRepository;
        }
        public async Task<Guid> AddPostAsync(PostViewModel postViewModel)
        {
            if (postViewModel is null)
                throw new ArgumentNullException(nameof(postViewModel));

            var post
                = postViewModel
                    .ToDomain();

            await _postRepository.AddAsync(post);

            return post.Id;
        }

        public async Task<PostViewModel> GetByIdAsync(Guid id)
        {
            var post
                = await _postRepository
                        .GetByIdAsync(id);

            return post.ToViewModel();
        }
    }
}
