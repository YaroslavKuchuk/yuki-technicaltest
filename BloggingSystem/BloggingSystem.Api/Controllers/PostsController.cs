using Blog.Domain;
using BloggingSystem.AppServices.Facades;
using BloggingSystem.Domain.Repositories.Abstractions;
using BloggingSystem.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BloggingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IPostService _postService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostViewModel>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var post = await _postService.GetByIdAsync(id);

        if (post is null) 
            return NotFound();

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] PostViewModel post, CancellationToken cancellationToken)
    {
        if (post is null) 
            return BadRequest($"{nameof(post)} is required!");

        var id = await _postService.AddPostAsync(post);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
}
