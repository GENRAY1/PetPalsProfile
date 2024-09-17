using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetPalsProfile.Application.Abstractions.Authentication;
using PetPalsProfile.Domain.Accounts;

namespace PetPalsProfile.Infrastructure.Authentication;

public class JwtProvider(
    IOptions<JwtOptions> options,
    RsaSecurityKey rsaSecurityKey
) : IJwtProvider
{
    public string GenerateAccessToken(Account user)
    {
        var signingCredentials = new SigningCredentials(
            key: rsaSecurityKey,
            algorithm: SecurityAlgorithms.RsaSha256
        );

        List<Claim> claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Email, user.Email),
        };
        
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }
        
        var token = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(options.Value.AccessTokenSettings.LifeTimeInMinutes),
            audience: options.Value.AccessTokenSettings.Audience,
            issuer: options.Value.AccessTokenSettings.Issuer,
            claims: claims
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var size = options.Value.RefreshTokenSettings.Length;
        var buffer = new byte[size];
        RandomNumberGenerator.Fill(buffer);
        return Convert.ToBase64String(buffer);
    }

    public int GetRefreshTokenLifetimeInMinutes()
    {
        return options.Value.RefreshTokenSettings.LifeTimeInMinutes;
    }

    public Guid GetUserIdFromToken(string token)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = options.Value.AccessTokenSettings.Issuer,
                ValidAudience = options.Value.AccessTokenSettings.Audience,
                IssuerSigningKey = rsaSecurityKey,
                ClockSkew = TimeSpan.FromMinutes(0)
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            var claims = jwtHandler.ValidateToken(token, tokenValidationParameters, out _);
            var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);

            return userId;
        }
        catch (Exception ex)
        {
            throw new Exception("Invalid token", ex);
        }
    }

    public bool IsTokenValid(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = options.Value.AccessTokenSettings.Issuer,
            ValidAudience = options.Value.AccessTokenSettings.Audience,
            IssuerSigningKey = rsaSecurityKey,
            ClockSkew = TimeSpan.FromMinutes(0)
        };

        var jwtHandler = new JwtSecurityTokenHandler();
        try
        {
            jwtHandler.ValidateToken(token, tokenValidationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}