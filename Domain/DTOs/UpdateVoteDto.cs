using Assignment1.Models;

namespace Assignment1.DTOs;

public class UpdateVoteDto
{
    public string Username { get; }
    public int PostId { get; }
    public bool? IsPositive { get; set; }

    public UpdateVoteDto(string username, int postId, bool? isPositive)
    {
        Username = username;
        PostId = postId;
        IsPositive = isPositive;
    }
}