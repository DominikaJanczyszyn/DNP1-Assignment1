using Application.DAO_Interfaces;
using Assignment1.DTOs;
using Assignment1.Models;

namespace FileDataAccess.DAOs;

public class CommentFileDao : ICommentDao
{
    private readonly FileContext _context;

    public CommentFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Comment> CreateAsync(Comment comment)
    {
        int id = 1;
        if (_context.Comments.Any())
        {
            id = _context.Comments.Max((c => c.Id));
            id++;
            Console.WriteLine(id);
        }

        comment.Id = id;
        
        _context.Comments.Add(comment);
        _context.SaveChanges();

        return Task.FromResult(comment);
    }

    public Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
    {
        IEnumerable<Comment> result = _context.Comments.AsEnumerable();
        if (searchParameters.PostId != null)
        {
            result = _context.Comments.Where(c => c.Post.Id == searchParameters.PostId);
        }
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            result = _context.Comments.Where(c => c.Author.Username.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(searchParameters.Body))
        {
            result = _context.Comments.Where(c => c.Body.Contains(searchParameters.Body, StringComparison.OrdinalIgnoreCase));
        }
        
        return Task.FromResult(result);
    }
}