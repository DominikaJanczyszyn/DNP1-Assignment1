using Application.DAO_Interfaces;
using Assignment1.DTOs;
using Assignment1.Models;

namespace FileDataAccess.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext _context;

    public PostFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Post?> CreateAsync(Post? post)
    {
        int id = 1;
        if (_context.Posts.Any())
        {
            id = _context.Posts.Max((p => p.Id));
            id++;
        }

        post.Id = id;
        
        _context.Posts.Add(post);
        _context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post?>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post?> result = _context.Posts.AsEnumerable();
                if (searchParameters.PostId != null)
                {
                    result = _context.Posts.Where(p => p.Id == searchParameters.PostId);
                }
                if (!string.IsNullOrEmpty(searchParameters.Username))
                {
                    result = _context.Posts.Where(p => p.Author.Username.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
                }
                if (!string.IsNullOrEmpty(searchParameters.TitleContains))
                {
                    result = _context.Posts.Where(p => p.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
                }
                if (!string.IsNullOrEmpty(searchParameters.BodyContains))
                {
                    result = _context.Posts.Where(p => p.Body.Contains(searchParameters.BodyContains, StringComparison.OrdinalIgnoreCase));
                }
        
                return Task.FromResult(result);
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(existing);
        
    }
}