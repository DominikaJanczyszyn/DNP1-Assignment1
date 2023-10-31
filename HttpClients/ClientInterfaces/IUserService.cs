using Assignment1.DTOs;
using Assignment1.Models;
using Shared.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAsync(string? usernameContains = null);
    Task CreateAsync(UserCreationDto dto);
}