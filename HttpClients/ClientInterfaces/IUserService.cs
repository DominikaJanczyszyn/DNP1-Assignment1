using Assignment1.DTOs;
using Assignment1.Models;
using Shared.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
    Task CreateAsync(UserCreationDto dto);
    Task<SearchUserParametersDto?> GetByUsernameAsync(string userName);
}