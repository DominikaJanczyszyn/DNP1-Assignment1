using Assignment1.Models;

namespace Application.DAO_Interfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
}