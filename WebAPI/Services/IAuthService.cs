using Assignment1.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task RegisterUser(User user);
}