namespace Assignment1.DTOs;

public class PostCreationDto
{
    public string AuthorUsername { get; }
    public string Title { get; }
    public string Body { get; }

    public PostCreationDto(string authorUsername, string title, string body)
    {
        AuthorUsername = authorUsername;
        Title = title;
        Body = body;
    }
}