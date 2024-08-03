using MediatR;

namespace PetPalsProfile.Application.Account.Login;

public class LoginAccountCommand:IRequest<LoginAccountResponse>
{
    public required string Email { get; init; }
    
    public required string Password { get; init; }
}