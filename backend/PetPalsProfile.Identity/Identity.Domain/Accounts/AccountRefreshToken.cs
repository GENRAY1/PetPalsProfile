namespace PetPalsProfile.Domain.Accounts;

public class AccountRefreshToken
{
    public required string Value { get; set; }
    
    public required DateTime ExpirationDate { get; set; }
    public required bool IsActive { get; set; }
}