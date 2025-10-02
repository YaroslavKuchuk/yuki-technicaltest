using Blog.Domain;
using BloggingSystem.AppServices.Facades;
using BloggingSystem.Domain.Repositories.Abstractions;
using BloggingSystem.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BloggingSystem.Api.Controllers;

#warning XML formatter is commented

[ApiController]
[Route("api/[controller]")]
// [Produces("application/json", "application/xml")]
public class PostController(IPostsService _postService) : ControllerBase
{
    [HttpGet("{id:guid}", Name = nameof(GetPostById))]
    public async Task<ActionResult<PostViewModel>> GetPostById(
        Guid id, 
        bool showAuthorInfo, 
        CancellationToken cancellationToken)
    {
        var post = await _postService.GetByIdAsync(id, showAuthorInfo);

        if (post is null) 
            return NotFound();

        return Ok(post);
    }

    [HttpPost]
    // [Consumes("application/json", "application/xml")]
    // [Produces("application/json", "application/xml")]
    public async Task<ActionResult<object>> Create([FromBody] PostViewModel post, CancellationToken cancellationToken)
    {
        if (post is null) 
            return BadRequest($"{nameof(post)} is required!");

        var id = await _postService.AddPostAsync(post);

        return CreatedAtAction(nameof(GetPostById), new { id }, new { id });
    }
}
