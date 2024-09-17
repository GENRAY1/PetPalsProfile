namespace PetPalsProfile.Domain.Accounts;

public class AccountRole
{
    public required Guid AccountId { get; init; }
    public required Guid RoleId { get; init; }
}