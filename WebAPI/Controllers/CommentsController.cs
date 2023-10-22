using Application.LogicInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentLogic _commentLogic;

    public CommentsController(ICommentLogic commentLogic)
    {
        _commentLogic = commentLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Comment>> CreateAsync([FromBody]CommentCreationDto dto)
    {
        try
        {
            Comment created = await _commentLogic.CreateAsync(dto);
            return Created($"/comments/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAsync([FromQuery]int? postId, [FromQuery] string? username, [FromQuery] string? body)
    {
        try
        {
            SearchCommentIdDto parameters = new(postId, username, body);
            var comments = await _commentLogic.GetAsync(parameters);
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}