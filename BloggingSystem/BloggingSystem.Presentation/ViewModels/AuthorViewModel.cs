namespace BloggingSystem.Presentation.ViewModels
{
    public sealed class AuthorViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}
