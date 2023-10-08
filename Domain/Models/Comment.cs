namespace Assignment1.Models;

public class Comment
{
    public int Id { get; set; }
    public User Author { get; set; }
    public Post Post { get; set; }
    public string Body { get; set; }

    public Comment( User author, Post post, string body)
    {
        Author = author;
        Post = post;
        Body = body;
    }
}