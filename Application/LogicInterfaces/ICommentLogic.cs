using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    Task<Comment> CreateAsync(CommentCreationDto dto);
    Task<IEnumerable<Comment>> GetAsync(SearchCommentIdDto searchParameters);
}