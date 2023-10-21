using Application.DAO_Interfaces;
using Application.LogicInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;
using Shared.DTOs;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao UserDao;

    public UserLogic(IUserDao userDao)
    {
        UserDao = userDao;
    }
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? exisiting = await UserDao.GetByUsernameAsync(dto.Username);
        if (exisiting != null)
        {
            throw new Exception("Username already taken!");
        }
        ValidateData(dto);
        User toCreate = new User(dto.Username, dto.Password);
        User created = await UserDao.CreateAsync(toCreate);
        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return UserDao.GetAsync(searchParameters);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        return UserDao.GetByUsernameAsync(userName);
    }

    public static void ValidateData(UserCreationDto dto)
    {
        string username = dto.Username;
        string password = dto.Password;
        
        if (username.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (username.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        if (password.Length < 8)
            throw new Exception("Password must be at least 3 characters!");

        if (password.Length > 16)
            throw new Exception("Password must be less than 16 characters!");
        
    }
}