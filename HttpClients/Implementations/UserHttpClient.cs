using System.Net.Http.Json;
using System.Text.Json;
using Assignment1.Models;
using HttpClients.ClientInterfaces;
using Assignment1.DTOs;
using Assignment1.Models;
namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly System.Net.Http.HttpClient _client;

    public UserHttpClient(HttpClient client)
    {
        this._client = client;
    }
    public async Task<IEnumerable<User>> GetUsers(string? usernameContains = null)
    {
        string uri = "/Users";
        if (!string.IsNullOrEmpty(usernameContains))
        {
            uri += $"?Username={usernameContains}";
        }
        HttpResponseMessage response = await _client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }

    public async Task CreateAsync(UserCreationDto dto)
    {
        HttpResponseMessage responseMessage = await _client.PostAsJsonAsync("/Users", dto);
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}