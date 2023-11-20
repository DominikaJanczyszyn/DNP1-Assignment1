using Application.DAO_Interfaces;
using Assignment1.Models;
using EfcDataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly PostContext context;

    public UserEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IQueryable<User> query = context.Users.AsQueryable();
        if (searchParameters.Username != null)
        {
            query = context.Users.Where(u => u.Username.Contains(searchParameters.Username));
        }
        IEnumerable<User> result = query.AsEnumerable();
        return result;
        
        
    }
}