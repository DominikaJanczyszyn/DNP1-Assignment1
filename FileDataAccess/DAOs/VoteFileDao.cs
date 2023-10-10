using Application.DAO_Interfaces;
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
}