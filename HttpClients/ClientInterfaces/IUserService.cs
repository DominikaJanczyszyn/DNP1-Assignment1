using Assignment1.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
}