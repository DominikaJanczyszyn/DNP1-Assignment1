using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.DAO_Interfaces;

public interface IVoteDao
{
    Task<Vote> CreateAsync(Vote vote);
    Task UpdateAsync(Vote vote);
    Task<IEnumerable<Vote>> GetAsync(SearchVoteParametersDto searchParameters);

    Task DeleteAsync(int id);
}