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

        int id = 1;
        if (_context.Votes.Any())
        {
            id = _context.Votes.Max((p => p.Id));
            id++;
        }

        vote.Id = id;
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

    public Task<IEnumerable<Vote>> GetAsync(SearchVoteParametersDto searchParameters)
    {
        IEnumerable<Vote> result = _context.Votes.AsEnumerable();
        if (searchParameters.PostId != null)
        {
            result = _context.Votes.Where(v => v.Post.Id== searchParameters.PostId);
        }

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            result = _context.Votes.Where(v => v.Author.Username.Equals(searchParameters.Username));
        }

        return Task.FromResult(result);
    }

    public Task DeleteAsync(int id)
    {
        Vote? result = _context.Votes.FirstOrDefault(v => v.Id == id);
        if (result == null)
        {
            throw new Exception("Vote not found!");
        }

        _context.Votes.Remove(result);
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}