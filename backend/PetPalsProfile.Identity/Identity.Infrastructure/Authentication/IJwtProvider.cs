using PetPalsProfile.Domain.UserAccounts;

namespace PetPalsProfile.Infrastructure.Authentication;

public interface IJwtProvider
{
    string GenerateToken(UserAccount user);
}