using Assignment1.Models;

namespace Application.DAO_Interfaces;

public interface IVoteDao
{
    Task<Vote> CreateAsync(Vote vote);
}