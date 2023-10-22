namespace Assignment1.DTOs;

public class SearchCommentIdDto
{
    public int? PostId { get; }
    public string? Username { get; }
    public string? Body { get; }

    public SearchCommentIdDto(int? postId, string? username, string? body)
    {
        PostId = postId;
        Username = username;
        Body = body;
    }
}