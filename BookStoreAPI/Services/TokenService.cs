using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryHours;

    // Constructor accepts the key, issuer, audience, and expiry duration
    public TokenService(string key, string issuer, string audience, int expiryHours = 8)
    {
        _key = key;
        _issuer = issuer;
        _audience = audience;
        _expiryHours = expiryHours;
    }

    // Method to generate the JWT token
    public string GenerateToken(string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
            }),
            Expires = DateTime.UtcNow.AddHours(_expiryHours),
            Issuer = _issuer, // Set the Issuer claim
            Audience = _audience, // Set the Audience claim
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            NotBefore = DateTime.UtcNow, // Add NotBefore claim to ensure the token is valid only after it's issued
            IssuedAt = DateTime.UtcNow, // Add IssuedAt claim for better validation
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
