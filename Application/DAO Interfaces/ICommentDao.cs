using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.DAO_Interfaces;

public interface ICommentDao
{
    Task<Comment> CreateAsync(Comment comment);
    Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters);
}