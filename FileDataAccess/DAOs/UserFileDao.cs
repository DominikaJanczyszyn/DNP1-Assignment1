using Application.DAO_Interfaces;
using Assignment1.Models;
using Shared.DTOs;

namespace FileDataAccess.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext _context;

    public UserFileDao(FileContext context)
    {
        _context = context;
    }
    public Task<User> CreateAsync(User user)
    {
        if (_context.Users.Any())
        {
            if (_context.Users.FirstOrDefault(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)) != null)
            {
                throw new Exception("User name is already taken!");
            }
        }
        _context.Users.Add(user);
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = _context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
        
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = _context.Users.AsEnumerable();
        if (searchParameters.Username != null)
        {
            users = _context.Users.Where(u => u.Username.Contains(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }
}