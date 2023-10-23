using Assignment1.Models;

namespace Assignment1.DTOs;

public class UpdateVoteDto
{
    public User Author { get; }
    public Post? Post { get; }
    public bool? IsPositive { get; set; }

    public UpdateVoteDto(User author, Post? post, bool? isPositive)
    {
        Author = author;
        Post = post;
        IsPositive = isPositive;
    }
}