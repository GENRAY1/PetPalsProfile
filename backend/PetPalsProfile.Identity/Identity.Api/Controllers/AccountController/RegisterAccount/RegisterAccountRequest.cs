namespace PetPalsProfile.Api.Controllers.AccountController.RegisterAccount;

public class RegisterAccountRequest
{
    public required string Email { get; init; }
    
    public required string Password { get; init; }
    
    public string? Phone { get; init; }
}