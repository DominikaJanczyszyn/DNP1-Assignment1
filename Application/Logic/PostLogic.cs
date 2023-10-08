using Application.DAO_Interfaces;
using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao PostDao;
    private readonly IUserDao UserDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        PostDao = postDao;
        UserDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await UserDao.GetByUsernameAsync(dto.AuthorUsername);
        if (user == null)
        {
            throw new Exception($"User with username {dto.AuthorUsername} does not exist.");
        }
        ValidatePost(dto);
        Post post = new Post(user, dto.Title, dto.Body);
        Post created = await PostDao.CreateAsync(post);

        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return PostDao.GetAsync(searchParameters);
    }

    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title can not be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body can not be empty.");
    }
}