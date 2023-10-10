using Assignment1.DTOs;
using Assignment1.Models;
using Shared.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}