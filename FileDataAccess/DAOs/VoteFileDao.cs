using Application.DAO_Interfaces;
using Assignment1.DTOs;
using Assignment1.Models;

namespace FileDataAccess.DAOs;

public class VoteFileDao : IVoteDao
{
    private readonly FileContext _context;

    public VoteFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Vote> CreateAsync(Vote vote)
    {
        
        _context.Votes.Add(vote);
        _context.SaveChanges();
        return Task.FromResult(vote);
    }

    public Task UpdateAsync(Vote vote)
    {
        
        Vote? result = _context.Votes.FirstOrDefault(v =>
            (v.Post.Id == vote.Post.Id && v.Author.Username == vote.Author.Username));
        if (result == null)
        {
            throw new Exception("Vote not found!");
        }

        _context.Votes.Remove(result);
        _context.Votes.Add(vote);
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Vote> GetAsync(SearchVoteParametersDto searchParameters)
    {
        Vote? result = _context.Votes.FirstOrDefault(v =>
            (v.Post.Id == searchParameters.PostId && v.Author.Username == searchParameters.Username));
        if (result == null)
        {
            throw new Exception("Vote not found!");
        }

        return Task.FromResult(result);
    }

    public Task DeleteAsync(int postId, string username)
    {
        Vote? result = _context.Votes.FirstOrDefault(v =>
            (v.Post.Id == postId && v.Author.Username == username));
        if (result == null)
        {
            throw new Exception("Vote not found!");
        }

        _context.Votes.Remove(result);
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}