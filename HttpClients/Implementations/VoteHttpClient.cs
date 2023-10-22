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

    public  async Task<Vote>  GetAsync(int postId, string username)
    {
        HttpResponseMessage response = await client.GetAsync($"/Votes");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Vote vote = JsonSerializer.Deserialize<Vote>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return vote;
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

    public async Task DeleteAsync(int postId, string username)
    {
        HttpResponseMessage response = await client.DeleteAsync("/Votes");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}