using Application.LogicInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VotesController : ControllerBase
{
    private readonly IVoteLogic _voteLogic;

    public VotesController(IVoteLogic voteLogic)
    {
        _voteLogic = voteLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Vote>> CreateAsync([FromBody] VoteCreationDto dto)
    {
        try
        {
            Vote created = await _voteLogic.CreateAsync(dto);
            return Created($"/votes/{created.Post}&{created.Author}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Vote>> GetAsync([FromQuery] int? postId, [FromQuery] string? username)
    {
        try
        {
            SearchVoteParametersDto dto = new SearchVoteParametersDto(postId, username);
            var vote = await _voteLogic.GetAsync(dto);
            return Ok(vote);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateVoteDto dto)
    {
        try
        {
            await _voteLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await _voteLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}