// TODO: adjust namespaces/types
using Blog.Domain;
using BloggingSystem.AppServices.Facades;
using BloggingSystem.Domain.Repositories.Abstractions;
using BloggingSystem.Presentation.Mapping;
using BloggingSystem.Presentation.ViewModels;
using FluentAssertions;
using Moq;

namespace BloggingSystem.Tests.Unit;

public class PostServiceTests
{
    [Test]
    public async Task GetByIdAsync_NoAuthor_WhenFlagFalse()
    {
        //Assign
        var postsRepository = new Mock<IPostRepository>();
        var authorRepository = new Mock<IAuthorRepository>();

        var author = new Author 
        { 
            Id = Guid.NewGuid(), 
            Name = "John", 
            Surname = "Doe" 
        };
        var post = new Post 
        { 
            Id = Guid.NewGuid(), 
            Title = "Test Title", 
            Description = "Test Description", 
            Content = "Test Content", 
            AuthorId = author.Id
        };
        postsRepository
            .Setup(r => r.GetByIdAsync(post.Id))
            .ReturnsAsync(post);
        var postsService = new PostsService(postsRepository.Object, authorRepository.Object);

        //Act
        var viewModel = await postsService.GetByIdAsync(post.Id, isUseAuthorInfo: false);
        
        //Assert
        viewModel.Should().NotBeNull();
        viewModel!.Author.Should().BeNull();
    }

    [Test]
    public async Task GetByIdAsync_WithAuthor_WhenFlagTrue()
    {
        //Assign
        var postsRepository = new Mock<IPostRepository>();
        var authorRepository = new Mock<IAuthorRepository>();

        var author = new Author 
        { 
            Id = Guid.NewGuid(), 
            Name = "John", 
            Surname = "Doe" 
        };
        var post = new Post 
        { 
            Id = Guid.NewGuid(), 
            Title = "Test Title", 
            Description = "Test Description", 
            Content = "Test Content", 
            AuthorId = author.Id,
            Author = author,
        };
        postsRepository
            .Setup(r => r.GetByIdAsync(post.Id))
            .ReturnsAsync(post);

        var postsService = new PostsService(postsRepository.Object, authorRepository.Object);

        //Act
        var viewModel = await postsService.GetByIdAsync(post.Id, isUseAuthorInfo: true);

        // Assert
        viewModel.Should().NotBeNull();
        viewModel!.Author.Should().NotBeNull();
        viewModel!.Author!.Id.Should().Be(author.Id);
    }

    [Test]
    public async Task AddPostAsync_Returns_New_Id()
    {
        //Assign
        var postsRepository = new Mock<IPostRepository>();
        var authorRepository = new Mock<IAuthorRepository>();
        postsRepository
            .Setup(r => r.AddAsync(It.IsAny<Post>()))
            .Returns(Task.FromResult(Guid.NewGuid()));
        var svc = new PostsService(postsRepository.Object, authorRepository.Object);

        var model = new PostViewModel 
        { 
            Title = "New Post", 
            Content = "World", 
            AuthorId = Guid.NewGuid() 
        };

        //Act
        var id = await svc.AddPostAsync(model);

        //Assert
        id.Should().NotBeEmpty();
        postsRepository.Verify(r => r.AddAsync(It.Is<Post>(p => p.Id == id && p.Title == "New Post")), Times.Once);
    }
}
