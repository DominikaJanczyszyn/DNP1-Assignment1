using System.Net.Http.Json;
using System.Text.Json;
using Assignment1.DTOs;
using Assignment1.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class CommentHttpClient : ICommentService
{
    private readonly HttpClient _client;

    public CommentHttpClient(HttpClient client)
    {
        this._client = client;
    }
    public async Task CreateAsync(CommentCreationDto dto)
    {
        HttpResponseMessage responseMessage = await _client.PostAsJsonAsync("/Comments", dto);
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Comment>> GetAsync(int id, string? username, string? body)
    {
        string query = ConstructQuery(id, username, body);
        HttpResponseMessage response = await _client.GetAsync("/Comments" + query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Comment> comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comments;
    }
    
    private static string ConstructQuery(int id, string? username, string? body)
    {
        string query = "";
        query += $"?postId={id}";
        if (!string.IsNullOrEmpty(username))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"username={username}";
        }
        if (!string.IsNullOrEmpty(body))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"body={body}";
        }
        Console.Write(query);
        return query;
    }
}