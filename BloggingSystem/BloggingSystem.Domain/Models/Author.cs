namespace Blog.Persistence;

public sealed class Author
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Surname { get; private set; }

    private Author() { }

    public Author(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        
        if (string.IsNullOrWhiteSpace(surname)) 
            throw new ArgumentNullException(nameof(surname));
        Name = name.Trim();
        Surname = surname.Trim();
    }

    public ICollection<Post> Posts { get; private set; } = new List<Post>();
}