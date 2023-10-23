namespace Assignment1.DTOs;

public class SearchCommentParametersDto
{
    public int PostId { get; }
    public string? Username { get; }
    public string? Body { get; }

    public SearchCommentParametersDto(int postId, string? username, string? body)
    {
        PostId = postId;
        Username = username;
        Body = body;
    }
}