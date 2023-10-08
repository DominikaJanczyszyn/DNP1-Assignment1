using Assignment1.Models;

namespace Application.DAO_Interfaces;

public interface ICommentDao
{
    Task<Comment> CreateAsync(Comment comment);
}