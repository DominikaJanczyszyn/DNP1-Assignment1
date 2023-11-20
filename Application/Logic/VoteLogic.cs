using Application.DAO_Interfaces;
using Application.LogicInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using Shared.DTOs;

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

        IEnumerable<Post?> result = await _postDao.GetAsync(new SearchPostParametersDto(dto.PostId, null, null, null));
        if (!result.Any())
        {
            throw new Exception($"Post with id {dto.PostId} does not exist.");
        }

        Post? post = result.First();

        Vote vote = new Vote(user, post, dto.IsPositive);
        Vote created = await _voteDao.CreateAsync(vote);
        return created;
    }

    public async Task UpdateAsync(UpdateVoteDto dto)
    {
        SearchVoteParametersDto searchVoteParametersDto = new SearchVoteParametersDto(dto.PostId, dto.Username);
        IEnumerable<Vote> voteData = await _voteDao.GetAsync(searchVoteParametersDto);
        Vote? existing = voteData.FirstOrDefault(v => (v.Post.Id == dto.PostId && v.Author.Username.Equals(dto.Username)));
        User? user = await _userDao.GetByUsernameAsync(dto.Username);
        SearchPostParametersDto postParametersDto = new SearchPostParametersDto(dto.PostId, null, null, null);
        IEnumerable<Post?> postData = await _postDao.GetAsync(postParametersDto);
        Post post = postData.First(p => p.Id == dto.PostId);
        if (existing == null)
        {
            throw new Exception("Vote not found!");
        }
        Vote updated;
        if ((dto.IsPositive == true))
        {
            updated = new Vote(user, post, true);
            updated.Id = existing.Id;
            await _voteDao.UpdateAsync(updated);
            // await _voteDao.DeleteAsync(existing.Id);
            //await _voteDao.CreateAsync(updated);
        }
        if (dto.IsPositive == false)
        {
            updated = new Vote(user,post, false);
            updated.Id = existing.Id;
            await _voteDao.UpdateAsync(updated);
            //await _voteDao.DeleteAsync(existing.Id);
            //await _voteDao.CreateAsync(updated);
        }
        if (dto.IsPositive == null)
        {
            await _voteDao.DeleteAsync(existing.Id);
        }
    }

    public Task<IEnumerable<Vote>> GetAsync(SearchVoteParametersDto searchParameters)
    {
        return _voteDao.GetAsync(searchParameters);
    }

    public async Task DeleteAsync(int id)
    {
        await _voteDao.DeleteAsync(id);
    }
}