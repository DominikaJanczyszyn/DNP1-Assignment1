using Application.DAO_Interfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using EfcDataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class VoteEfcDao : IVoteDao
{
    private readonly PostContext context;

    public VoteEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<Vote> CreateAsync(Vote vote)
    {
        EntityEntry<Vote> added = await context.Votes.AddAsync(vote);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task UpdateAsync(Vote vote)
    {
        var existingVote = await context.Votes.FindAsync(vote.Id);
        context.Entry(existingVote).State = EntityState.Detached;
        context.Entry(existingVote).CurrentValues.SetValues(vote);
        context.Entry(existingVote).State = EntityState.Modified;
        await context.SaveChangesAsync();

    }

    public  async Task<IEnumerable<Vote>> GetAsync(SearchVoteParametersDto searchParameters)
    {
        IQueryable<Vote> query = context.Votes.Include(vote => vote.Post).Include(vote => vote.Author).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            query = query.Where(v =>
                v.Author.Username.Contains(searchParameters.Username));
        }
        if (searchParameters.PostId != null)
        {
            query = query.Where(v =>v.Post.Id == searchParameters.PostId);
        }

        List<Vote> result = await query.ToListAsync();
        return result;
    }

    public async Task DeleteAsync(int id)
    {
        Vote? existing = context.Votes.FirstOrDefault(v => v.Id == id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} not found");
        }

        context.Votes.Remove(existing);
        await context.SaveChangesAsync();
    }
}