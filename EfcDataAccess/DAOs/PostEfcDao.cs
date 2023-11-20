using Application.DAO_Interfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using EfcDataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<Post?> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        Console.WriteLine(post.Author.Username);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post?>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Author).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            query = query.Where(c =>
                c.Author.Username.ToLower().Contains(searchParameters.Username.ToLower()));
        }
        if (searchParameters.PostId != null)
        {
            query = query.Where(p => p.Id == searchParameters.PostId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.Title))
        {
            query = query.Where(p =>
                p.Title.ToLower().Contains(searchParameters.Title.ToLower()));
        }
        if (!string.IsNullOrEmpty(searchParameters.Body))
        {
            query = query.Where(p =>
                p.Body.ToLower().Contains(searchParameters.Body.ToLower()));
        }

        IEnumerable<Post> result = await query.ToListAsync();
        return result;
    }
}