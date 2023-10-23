using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.DAO_Interfaces;

public interface IPostDao
{
    Task<Post?> CreateAsync(Post? post);
    Task<IEnumerable<Post?>> GetAsync(SearchPostParametersDto searchParameters);
    Task<Post?> GetByIdAsync(int id);
}