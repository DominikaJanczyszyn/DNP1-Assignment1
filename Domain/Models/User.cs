using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignment1.Models;

public class User
{
    [Key]
    public string Username { get; set; }
    public string Password { get; set; }
    [JsonIgnore]
    public ICollection<Post> Posts{ get; set; }
    [JsonIgnore]
    public ICollection<Vote> Votes { get; set; }
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
}