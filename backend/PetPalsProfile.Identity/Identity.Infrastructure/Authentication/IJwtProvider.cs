using PetPalsProfile.Domain.Accounts;
using PetPalsProfile.Domain.UserAccounts;

namespace PetPalsProfile.Infrastructure.Authentication;

public interface IJwtProvider
{
    string GenerateToken(Account user);
}