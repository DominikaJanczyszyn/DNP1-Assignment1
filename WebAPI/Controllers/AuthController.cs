using System.Security.Claims;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.LogicInterfaces;
using Assignment1.DTOs;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserLogic _userLogic;

    public AuthController(IConfiguration config, IUserLogic userLogic)
    {
        _config = config;
        _userLogic = userLogic;
    }
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            User? user = await _userLogic.GetByUsernameAsync(userLoginDto.Username);
            if(user.Password.Equals(userLoginDto.Password) )
            {
               string token = GenerateJwt(user); 
               return Ok(token);
            }
         
            
            return BadRequest("Incorect password.");
            
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    private List<Claim> GenerateClaims(User? user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };
        return claims.ToList();
    }
    
    private string GenerateJwt(User? user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
}