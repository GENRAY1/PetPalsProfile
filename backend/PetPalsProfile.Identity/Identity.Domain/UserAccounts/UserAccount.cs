using PetPalsProfile.Domain.Absractions;
using PetPalsProfile.Domain.Roles;

namespace PetPalsProfile.Domain.UserAccounts;

public class UserAccount : Entity
{
    public const int MaxEmailLength = 256;
    public const int MaxPasswordLength = 64;
    public const int MinPasswordLength = 6;
    public const int MinUserNameLength = 6;
    public const int MaxUserNameLength = 32;

    private UserAccount(Guid id) : base(id) { }
    public string? Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }

    public static UserAccount Create(string email, string passwordHash, string? username)
    {
        return new UserAccount(Guid.NewGuid())
        {
            Email = email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            RoleId = Role.User.Id,
            Username = username
        };
    }
}