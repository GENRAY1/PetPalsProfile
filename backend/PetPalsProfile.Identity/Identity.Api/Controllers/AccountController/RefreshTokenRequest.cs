namespace PetPalsProfile.Api.Controllers.AccountController;

public class RefreshTokenRequest
{
    public required string RefreshToken { get; init; }
    
    public required string AccessToken { get; set; }
}