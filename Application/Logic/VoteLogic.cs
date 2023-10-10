using Application.DAO_Interfaces;
using Application.LogicInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.Logic;

public class VoteLogic : IVoteLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao _userDao;
    private readonly IVoteDao _voteDao;

    public VoteLogic(IPostDao postDao, IUserDao userDao, IVoteDao voteDao)
    {
        _postDao = postDao;
        _userDao = userDao;
        _voteDao = voteDao;
    }

    public async Task<Vote> CreateAsync(VoteCreationDto dto)
    {
        User? user = await _userDao.GetByUsernameAsync(dto.AuthorUsername);
        if (user == null)
        {
            throw new Exception($"User with username {dto.AuthorUsername} does not exist.");
        }

        IEnumerable<Post> result = await _postDao.GetAsync(new SearchPostParametersDto(dto.PostId, null, null, null));
        if (!result.Any())
        {
            throw new Exception($"Post with id {dto.PostId} does not exist.");
        }

        Post post = result.First();

        Vote vote = new Vote(user, post, dto.isPositive);
        Vote created = await _voteDao.CreateAsync(vote);
        return created;
    }
}