using Blog.Domain;
using BloggingSystem.Presentation.ViewModels;

namespace BloggingSystem.Presentation.Mapping
{
    public static class AuthorExtension
    {
        public static AuthorViewModel ToViewModel(this Author author)
        {
            if (author is null)
                return null;

            var viewModel = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Posts = author
                            .Posts != null
                                ? author
                                    .Posts
                                        .Select(p => p.ToViewModel())
                                        .ToList()
                                : Enumerable.Empty<PostViewModel>().ToList(),
                Surname = author.Surname
            };

            return viewModel;
        }

        public static Author ToDomain(this AuthorViewModel viewModel)
        {
            if (viewModel is null)
                return null;

            var author = new Author
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Posts = viewModel
                            .Posts != null
                                ? viewModel
                                    .Posts
                                        .Select(p => p.ToDomain())
                                        .ToList()
                                 : Enumerable.Empty<Post>().ToList(),
                Surname = viewModel.Surname
            };

            return author;
        }
    }
}
