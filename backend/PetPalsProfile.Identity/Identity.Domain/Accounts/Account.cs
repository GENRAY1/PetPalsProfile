using PetPalsProfile.Domain.Absractions;
using PetPalsProfile.Domain.UserAccounts;

namespace PetPalsProfile.Domain.Accounts;

public class Account : Entity
{
    public const int MaxEmailLength = 256;
    public const int MaxPasswordLength = 64;
    public const int MinPasswordLength = 6;
    
    public const int MaxPhoneLength = 32;

    private Account(Guid id) : base(id) { }
    public string? Phone { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public AccountRefreshToken? RefreshToken { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }

    public void SetRefreshToken(AccountRefreshToken refreshToken)
    {
        RefreshToken = refreshToken;
    }
    public void DisableRefreshToken()
    {
        if (RefreshToken is not null)
        {
            RefreshToken.IsActive = false;
        }
    }
    
    public void ActivateRefreshToken()
    {
        if (RefreshToken is not null)
        {
            RefreshToken.IsActive = true;
        }
    }
    

    public static Account Create(string email, string passwordHash, string? phone)
    {
        return new Account(Guid.NewGuid())
        {
            Email = email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            RoleId = Role.User.Id,
            Phone = phone
        };
    }
}