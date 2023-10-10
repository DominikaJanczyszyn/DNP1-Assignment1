namespace Assignment1.DTOs;

public class VoteCreationDto
{
    public string AuthorUsername { get; }
    public int PostId{ get; }
    public bool isPositive { get; }

    public VoteCreationDto(string authorUsername, int postId, bool isPositive)
    {
        AuthorUsername = authorUsername;
        PostId = postId;
        this.isPositive = isPositive;
    }
}