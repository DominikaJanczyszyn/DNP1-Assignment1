using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.Logic;

public interface IPostLogic
{
    Task<Post?> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post?>> GetAsync(SearchPostParametersDto searchParameters);
}