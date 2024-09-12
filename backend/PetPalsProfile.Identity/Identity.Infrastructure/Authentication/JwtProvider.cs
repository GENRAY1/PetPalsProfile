using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetPalsProfile.Domain.Accounts;
using PetPalsProfile.Domain.UserAccounts;

namespace PetPalsProfile.Infrastructure.Authentication;


public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    public string GenerateToken(Account user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Secret)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(options.Value.ExpireHours),
            audience: options.Value.Audience,
            issuer: options.Value.Issuer,
            claims: new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.RoleId.ToString())
            }
        );
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenString;
    }
}