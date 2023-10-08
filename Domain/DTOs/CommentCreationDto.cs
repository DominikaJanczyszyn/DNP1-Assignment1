namespace Assignment1.DTOs;

public class CommentCreationDto
{
    public string AuthorUsername { get; }
    public int PostId{ get; }
    public string Body { get; }

    public CommentCreationDto(string authorUsername, int postId, string body)
    {
        AuthorUsername = authorUsername;
        PostId = postId;
        Body = body;
    }
}