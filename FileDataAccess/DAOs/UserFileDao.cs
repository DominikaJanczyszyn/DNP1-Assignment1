using Application.DAO_Interfaces;
using Assignment1.Models;

namespace FileDataAccess.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext Context;

    public UserFileDao(FileContext context)
    {
        Context = context;
    }
    public Task<User> CreateAsync(User user)
    {
        if (Context.Users.Any())
        {
            if (Context.Users.FirstOrDefault(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)) != null)
            {
                throw new Exception("User name is already taken!");
            }
        }
        Context.Users.Add(user);
        Context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = Context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
        
    }
}