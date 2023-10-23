using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Assignment1.DTOs;
using Assignment1.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class VoteHttpClient : IVoteService
{
    private readonly HttpClient client;

    public VoteHttpClient(HttpClient client)
    {
        this.client = client;
    }
    public async Task CreateAsync(VoteCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Votes", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public  async Task<ICollection<Vote>>  GetAsync(int? postId, string? username)
    {
        string query = ConstructQuery(postId, username);
        HttpResponseMessage response = await client.GetAsync("/Votes"+ query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Vote> votes = JsonSerializer.Deserialize<ICollection<Vote>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return votes;
    }

    public async Task UpdateAsync(UpdateVoteDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/Votes", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        };
    }

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"/Votes/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    private static string ConstructQuery(int? id, string? username)
    {
        string query = "";
        if (id != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"?postId={id}";
        }
        if (!string.IsNullOrEmpty(username))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"username={username}";
        }
        Console.Write(query);
        return query;
    }
}