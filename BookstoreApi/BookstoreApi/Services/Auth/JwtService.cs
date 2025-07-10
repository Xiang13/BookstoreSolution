using BookstoreApi.Models.DTOs.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService
{
    private readonly string _secretKey;
    private readonly int _expiryMinutes;

    public JwtService(IConfiguration config)
    {
        _secretKey = config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing");
        _expiryMinutes = int.Parse(config["Jwt:ExpiryMinutes"] ?? "60");
    }

    public string GenerateToken(UserDTO user)
    {
        var claims = new List<Claim>
        {
            new Claim("displayName", user.DisplayName),
            new Claim("userId", user.UserId.ToString()),
        }
        ;   
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim("role", role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
