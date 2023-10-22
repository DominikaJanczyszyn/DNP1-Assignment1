using Assignment1.DTOs;
using Assignment1.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(
        int? id, 
        string? username,
        string? titleContains,
        string? bodyContains
    );
    Task<SearchPostParametersDto?> GetByIdAsync(int id);
}