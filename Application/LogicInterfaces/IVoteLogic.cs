using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.LogicInterfaces;

public interface IVoteLogic
{
    Task<Vote> CreateAsync(VoteCreationDto dto);
}