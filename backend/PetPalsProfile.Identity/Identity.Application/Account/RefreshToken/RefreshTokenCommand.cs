using MediatR;

namespace PetPalsProfile.Application.Account.RefreshToken;

public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
{
    public required string RefreshToken { get; init; }
    
    public required string AccessToken { get; set; }
}