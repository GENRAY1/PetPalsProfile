namespace PetPalsProfile.Api.Controllers.AccountController;

public class LoginAccountRequest
{
    public required string Email { get; init; }
    
    public required string Password { get; init; }
}