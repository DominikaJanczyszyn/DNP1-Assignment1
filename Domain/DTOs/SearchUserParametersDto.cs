namespace Shared.DTOs;

public class SearchUserParametersDto
{
    public string? Username { get;  }

    public SearchUserParametersDto(string? username)
    {
        Username = username;
    }
}