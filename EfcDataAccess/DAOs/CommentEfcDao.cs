using Application.DAO_Interfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using EfcDataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    private readonly PostContext context;

    public CommentEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<Comment> CreateAsync(Comment comment)
    {
        EntityEntry<Comment> added = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public  async Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
    {
        IQueryable<Comment> query = context.Comments.Include(c => c.Post).Include(vote => vote.Author).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            query = query.Where(c =>
                c.Author.Username.ToLower().Contains(searchParameters.Username.ToLower()));
        }
        if (searchParameters.PostId != null)
        {
            query = query.Where(c =>c.Post.Id == searchParameters.PostId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.Body))
        {
            query = query.Where(c =>
                c.Body.ToLower().Contains(searchParameters.Body.ToLower()));
        }

        List<Comment> result = await query.ToListAsync();
        return result;
    }
}