namespace Assignment1.DTOs;

public class SearchPostParametersDto
{
 public int? PostId { get; }
 public string? Username { get; }
 public string? TitleContains { get; }
 public string? BodyContains { get; }

 public SearchPostParametersDto(int? postId, string? username, string? titleContains, string? bodyContains)
 {
  PostId = postId;
  Username = username;
  TitleContains = titleContains;
  BodyContains = bodyContains;
 }
}