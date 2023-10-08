using Assignment1.DTOs;
using Assignment1.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);
}