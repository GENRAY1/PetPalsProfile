namespace PetPalsProfile.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string GenerateAccessToken(Domain.Accounts.Account user);
    string GenerateRefreshToken();
    int GetRefreshTokenLifetimeInMinutes();
    Guid GetUserIdFromToken(string token);
    bool IsTokenValid(string token);
}