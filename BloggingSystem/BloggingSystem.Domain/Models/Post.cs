namespace Blog.Persistence;

public sealed class Post
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid AuthorId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string Content { get; private set; }

    public Author? Author { get; private set; }

    private Post() { }

    public Post(
        Guid authorId, 
        string title, 
        string description, 
        string content)
    {
        if (string.IsNullOrWhiteSpace(title)) 
            throw new ArgumentNullException(nameof(title));

        if (string.IsNullOrWhiteSpace(content)) 
            throw new ArgumentNullException(nameof(content));

        AuthorId = authorId;
        Title = title;
        Description = 
            string.IsNullOrWhiteSpace(description) 
                ? null 
                : description.Trim();
        Content = content.Trim();
    }
}