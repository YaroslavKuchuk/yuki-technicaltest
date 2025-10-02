using BloggingSystem.Domain.Repositories.Abstractions;
using BloggingSystem.Presentation.Mapping;
using BloggingSystem.Presentation.ViewModels;

namespace BloggingSystem.AppServices.Facades
{
    public interface IPostsService
    {
        Task<Guid> AddPostAsync(PostViewModel postViewModel);

        Task<PostViewModel> GetByIdAsync(Guid id, bool isUseAuthorInfo = false);
    }
    public sealed class PostsService : IPostsService
    {
        public readonly IPostRepository _postRepository;

        public readonly IAuthorRepository _authorRepository;
        public PostsService(IPostRepository postRepository,
            IAuthorRepository authorRepository) 
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
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

        public async Task<PostViewModel> GetByIdAsync(Guid id, bool isUseAuthorInfo = false)
        {
            var post
                = await _postRepository
                        .GetByIdAsync(id);

            var postViewModel =  post.ToViewModel();

            if (isUseAuthorInfo)
            {
                var authorViewModel
                    = post.Author?.ToViewModel();

                postViewModel.Author = authorViewModel;
            }

            return postViewModel;
        }
    }
}
