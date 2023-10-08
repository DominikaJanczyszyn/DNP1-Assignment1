namespace Assignment1.Models;

public class Post
{
    public int Id { get; set; }
    public User Author { get; }
    public string Title { get;  }
    public string Body { get; set; }

    public Post( User author, string title, string body)
    {
        Author = author;
        Title = title;
        Body = body;
    }

    public string toString()
    {
        return $"{Id}, {Title}, {Body}";
    }
}