using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SLMS.DOL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SLMS.BLL.Helpers;

public class JwtTokenHelper
{
    private readonly IConfiguration _config;

    public JwtTokenHelper(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user, string roleName)
    {
        var claims = new[]
        {
            // Stores logged-in user's database Id
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            // Stores logged-in username
            new Claim(ClaimTypes.Name, user.Username),

            // Stores user's role: Admin / Librarian / User
            new Claim(ClaimTypes.Role, roleName)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(
                Convert.ToDouble(_config["Jwt:ExpiryMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}