using Application.LogicInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class VoteController : ControllerBase
{
    private readonly IVoteLogic _voteLogic;

    public VoteController(IVoteLogic voteLogic)
    {
        _voteLogic = voteLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Vote>> CreateAsync([FromBody]VoteCreationDto dto)
    {
        try
        {
            Vote created = await _voteLogic.CreateAsync(dto);
            return Created($"/votes/{created.Post}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}