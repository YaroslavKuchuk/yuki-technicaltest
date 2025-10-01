using Blog.Domain;
using BloggingSystem.Presentation.ViewModels;

namespace BloggingSystem.Presentation.Mapping
{
    public static class PostExtensions
    {
        public static PostViewModel ToViewModel(this Post post)
        {
            if (post is null)
                return null;

            var viewModel = new PostViewModel
            {
                AuthorId = post.AuthorId,
                Content = post.Content,
                Description = post.Description,
                Id = post.Id,
                Title = post.Title
            };

            return viewModel;
        }

        public static Post ToDomain(this PostViewModel postViewModel)
        {
            if (postViewModel is null)
                return null;

            var post = new Post
            {
                AuthorId = postViewModel.AuthorId,
                Content = postViewModel.Content,
                Description = postViewModel.Description,
                Id = postViewModel.Id,
                Title = postViewModel.Title
            };

            return post;
        }
    }
}
