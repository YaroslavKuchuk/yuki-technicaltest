namespace BloggingSystem.Presentation.ViewModels
{
    public sealed class PostViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AuthorId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }

        public AuthorViewModel? Author { get; set; }
    }
}
