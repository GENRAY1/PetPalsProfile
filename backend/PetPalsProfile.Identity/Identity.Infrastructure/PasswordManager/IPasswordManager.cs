namespace PetPalsProfile.Infrastructure.PasswordManager;

public interface IPasswordManager
{
    string Generate(string password);
    bool Verify(string password, string hash);
}