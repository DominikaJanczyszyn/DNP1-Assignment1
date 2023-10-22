using Assignment1.DTOs;
using Assignment1.Models;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task CreateAsync(CommentCreationDto dto);
    Task<ICollection<Comment>> GetAsync(SearchCommentIdDto searchParameters);
}