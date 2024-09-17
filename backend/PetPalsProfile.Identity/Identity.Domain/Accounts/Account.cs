using PetPalsProfile.Domain.Absractions;

namespace PetPalsProfile.Domain.Accounts;

public class Account : Entity
{
    public const int MaxEmailLength = 256;
    public const int MaxPasswordLength = 64;
    public const int MinPasswordLength = 6;
    public const int MaxPhoneLength = 32;

    private List<Role> _roles = new();

    private Account(Guid id) : base(id)
    {
    }

    private Account()
    {
    }

    public string? Phone { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public AccountRefreshToken? RefreshToken { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IReadOnlyCollection<Role> Roles => _roles;

    public void AddRole(Role role)
    {
        if (!_roles.Contains(role))
        {
            _roles.Add(role);
        }
    }

    public void RemoveRole(Role role)
    {
        _roles.Remove(role);
    }

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

    public static Account Create(string email, string passwordHash, string? phone)
    {
        Account account = new Account(Guid.NewGuid())
        {
            Email = email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            Phone = phone
        };
        
        account.AddRole(Role.User);
        
        return account;
    }
}