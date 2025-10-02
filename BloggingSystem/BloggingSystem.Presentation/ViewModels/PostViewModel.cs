using System.ComponentModel.DataAnnotations;

namespace BloggingSystem.Presentation.ViewModels
{
    public sealed class PostViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AuthorId { get; set; }

        [Required, StringLength(200, MinimumLength = 1)]
        public string? Title { get; set; }

        [Required, StringLength(500, MinimumLength = 1)]
        public string? Description { get; set; }

#warning - in Data Model we should get rid of varchar(max)
        [Required, StringLength(20000, MinimumLength = 1)]
        public string? Content { get; set; }

        public AuthorViewModel? Author { get; set; }
    }
}
