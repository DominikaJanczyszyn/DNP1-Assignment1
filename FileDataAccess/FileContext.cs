using System.Diagnostics.Tracing;
using System.Text.Json;
using Assignment1.Models;

namespace FileDataAccess;

public class FileContext
{
    private const string FilePath = "data.json";
    private DataContainer? _dataContainer;

    public ICollection<Post?> Posts
    {
        get
        {
            LoadData();
            return _dataContainer!.Posts;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return _dataContainer!.Users;
        }
    }

    public ICollection<Comment> Comments
    {
        get
        {
            LoadData();
            return _dataContainer!.Comments;
        }
    }

    public ICollection<Vote> Votes
    {
        get
        {
            LoadData();
            return _dataContainer!.Votes;
        }
    }

    private void LoadData()
    {
        if(_dataContainer!=null) return;

        if (!File.Exists(FilePath))
        {
            _dataContainer = new()
            {
                Posts = new List<Post>(),
                Users = new List<User>(),
                Comments = new List<Comment>(),
                Votes = new List<Vote>()
            };
            return;
        }
        string content = File.ReadAllText(FilePath);
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(_dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, serialized);
        _dataContainer = null;
    }
}