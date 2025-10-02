using BloggingSystem.Presentation.ViewModels;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BloggingSystem.Tests.Integration;

public class PostsApiTests
{
    [Test]
    public async Task Create_Then_Get_With_And_Without_Author()
    {
        using var app = new Infrastructure.CustomWebAppFactory();

        //Assign
        var httpClient = app.CreateClient();

        var viewModel = new PostViewModel 
        { 
            Title = "Test Title", 
            Description = "Test Desc", 
            Content = "Test Content", 
            AuthorId = Guid.NewGuid() 
        };

        //Act + Assert
        var postResponse = await httpClient.PostAsJsonAsync("/post", viewModel);
        Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var payload = await postResponse.Content.ReadFromJsonAsync<Dictionary<string, Guid>>();
        Assert.That(payload, Is.Not.Null);
        var id = payload!["id"];

        var getResponseWithoutAuthor = await httpClient.GetAsync($"/post/{id}?showAuthorInfo=false");
        Assert.That(getResponseWithoutAuthor.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var getResponseWithAuthor = await httpClient.GetAsync($"/post/{id}?showAuthorInfo=true");
        Assert.That(getResponseWithAuthor.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Get_Unknown_Returns_404()
    {
        using var app = new Infrastructure.CustomWebAppFactory();

        //Assign
        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/post/{Guid.NewGuid()}");

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
