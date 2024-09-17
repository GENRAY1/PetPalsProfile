using PetPalsProfile.Application.Abstractions.PasswordManager;

namespace PetPalsProfile.Infrastructure.PasswordManager;

public class PasswordManager : IPasswordManager
{
    public string Generate(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
    
    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}