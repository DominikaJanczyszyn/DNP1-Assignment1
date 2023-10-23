using Application.Logic;
using Application.LogicInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
     private readonly IPostLogic _postLogic;

    public PostsController(IPostLogic postLogic)
    {
        _postLogic = postLogic;
    }
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreationDto dto)
    {
        try
        {
            Post? created = await _postLogic.CreateAsync(dto);
            return Created($"/posts/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery]int? postId, [FromQuery] string? username, [FromQuery] string? titleContains, [FromQuery] string? bodyContains)
    {
        try
        {
            SearchPostParametersDto parameters = new(postId, username, titleContains, bodyContains);
            var posts = await _postLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetByIdAsync([FromQuery]int id)
    {
        try
        {
            Post? post = await _postLogic.GetByIdAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}