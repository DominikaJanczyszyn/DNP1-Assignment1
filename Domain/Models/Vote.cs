namespace Assignment1.Models;

public class Vote
{
    public User Author { get; set; }
    public Post Post { get; set; }
    public bool IsPositive { get; set; }

    public Vote(User author, Post post, bool isPositive)
    {
        Author = author;
        Post = post;
        IsPositive = isPositive;
    }
    
}