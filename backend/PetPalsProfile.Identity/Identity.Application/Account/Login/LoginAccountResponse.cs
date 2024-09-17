namespace PetPalsProfile.Application.Account.Login;

public class LoginAccountResponse
{
    public required string RefreshToken { get; init; }
    public required string AccessToken { get; init; }
}