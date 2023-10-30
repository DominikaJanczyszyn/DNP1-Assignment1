namespace Assignment1.DTOs;

public class SearchPostParametersDto
{
 public int? PostId { get; }
 public string? Username { get; }
 public string? Title { get; }
 public string? Body { get; }

 public SearchPostParametersDto(int? postId, string? username, string? title, string? body)
 {
  PostId = postId;
  Username = username;
  Title = title;
  Body = body;
 }
}