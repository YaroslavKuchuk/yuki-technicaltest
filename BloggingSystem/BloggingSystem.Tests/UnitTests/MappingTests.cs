// TODO: adjust namespaces/types
using FluentAssertions;
using Blog.Domain;
using BloggingSystem.Presentation.ViewModels;
using BloggingSystem.Presentation.Mapping;

namespace BloggingSystem.Tests.Unit;

public class MappingTests
{
    [Test]
    public void Post_To_ViewModel_Maps_All_Fields()
    {
        //Assign
        var author = new Author 
        { 
            Id = Guid.NewGuid(), 
            Name = "John", 
            Surname = "Doe" 
        };

        var post = new Post
        {
            Id = Guid.NewGuid(),
            Title = "Test title",
            Description = "Test description",
            Content = "Test content",
            AuthorId = author.Id,
            Author = author
        };

        //Act
        var vm = post.ToViewModel();

        //Assert
        vm.Id.Should().Be(post.Id);
        vm.Title.Should().Be("Test title");
        vm.Description.Should().Be("Test description");
        vm.Content.Should().Be("Test content");
    }

    [Test]
    public void ViewModel_To_Entity_Maps_All_Fields()
    {
        //Assign
        var vm = new PostViewModel 
        { 
            Id = Guid.NewGuid(), 
            Title = "Test Title", 
            Description = "Test Description", 
            Content = "Test Content", 
            AuthorId = Guid.NewGuid() 
        };
        
        //Act 
        var entity = vm.ToDomain();

        //Assert
        entity.Id.Should().Be(vm.Id);
        entity.Title.Should().Be("Test Title");
        entity.Description.Should().Be("Test Description");
        entity.Content.Should().Be("Test Content");
        entity.AuthorId.Should().Be(vm.AuthorId);
    }
}
