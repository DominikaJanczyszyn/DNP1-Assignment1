using Assignment1.DTOs;
using Assignment1.Models;
using Shared.DTOs;

namespace WebAPI.Controllers;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UsersController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/users/{user.Username}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
    {
        try
        {
            SearchUserParametersDto parameters = new(username);
            IEnumerable<User> users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    /*[HttpGet]
    public async Task<ActionResult<User>> GetByUsernameAsync( [FromQuery] string username)
    {
        try
        {
            SearchUserParametersDto parameters = new(username);
            User? user = await userLogic.GetByUsernameAsync(username);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }*/
}