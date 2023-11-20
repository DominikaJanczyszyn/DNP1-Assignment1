using System.Net.Http.Json;
using System.Text.Json;
using Assignment1.DTOs;
using Assignment1.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient _client;

    public PostHttpClient(HttpClient client)
    {
        this._client = client;
    }
    public async Task CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage responseMessage = await _client.PostAsJsonAsync("/Posts", dto);
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<IEnumerable<Post?>> GetAsync(int? id, string? username, string? titleContains, string? bodyContains)
    {
        HttpResponseMessage response = await _client.GetAsync("/Posts");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        IEnumerable<Post?> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine(posts);
        return posts;
    }
}