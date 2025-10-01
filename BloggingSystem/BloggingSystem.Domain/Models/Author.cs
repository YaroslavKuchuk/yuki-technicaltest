namespace Blog.Domain;

public sealed class Author
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}