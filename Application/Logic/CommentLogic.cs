using Application.DAO_Interfaces;
using Application.LogicInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.Logic;

public class CommentLogic : ICommentLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao _userDao;
    private readonly ICommentDao _commentDao;

    public CommentLogic(IPostDao postDao, IUserDao userDao, ICommentDao commentDao)
    {
        _postDao = postDao;
        _userDao = userDao;
        _commentDao = commentDao;
    }

    public async Task<Comment> CreateAsync(CommentCreationDto dto)
    {
        User? user = await _userDao.GetByUsernameAsync(dto.AuthorUsername);
        if (user == null)
        {
            throw new Exception($"User with username {dto.AuthorUsername} does not exist.");
        }

        IEnumerable<Post?> result = await _postDao.GetAsync(new SearchPostParametersDto(dto.PostId, null, null, null));
        if (!result.Any())
        {
            throw new Exception($"Post with id {dto.PostId} does not exist.");
        }

        Post? post = result.First();
        ValidateComment(dto);
        Comment comment = new Comment(user, post, dto.Body);
        Comment created = await _commentDao.CreateAsync(comment);
        return created;
    }

    public Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
    {
       return _commentDao.GetAsync(searchParameters);
    }

    private void ValidateComment(CommentCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body can not be empty.");
    }
    
    
}