using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignment1.Models;

public class Post
{
    
    [Key]
    public int Id { get; set; }
    public User Author { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    [JsonIgnore]
    public ICollection<Vote> Votes { get; set; }
    [JsonIgnore]
    public ICollection<Comment> Comments { get; set; }

    public Post( User author, string? title, string? body)
    {
        Author = author;
        Title = title;
        Body = body;
    }
    private Post(){}
}