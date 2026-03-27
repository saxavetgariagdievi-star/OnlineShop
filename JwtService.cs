using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Modules;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateTokenAsync(User user)
    {
        var keyString = _configuration["JwtSettings:Key"]
            ?? throw new InvalidOperationException("JWT Key boş ola bilməz");

        var issuer = _configuration["JwtSettings:Issuer"]
            ?? throw new InvalidOperationException("JWT Issuer boş ola bilməz");

        var audience = _configuration["JwtSettings:Audience"]
            ?? throw new InvalidOperationException("JWT Audience boş ola bilməz");

        var expireMinutes = int.TryParse(_configuration["JwtSettings:ExpireMinutes"], out var exp) ? exp : 60;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(tokenString);
    }
}