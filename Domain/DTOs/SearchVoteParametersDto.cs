namespace Assignment1.DTOs;

public class SearchVoteParametersDto
{
    public int? PostId { get; }
    public string? Username { get; }


    public SearchVoteParametersDto(int? postId, string? username)
    {
        PostId = postId;
        Username = username;
    }
}