using MediatR;

namespace PetPalsProfile.Application.Account.Register;

public class RegisterAccountCommand : IRequest<RegisterAccountResponse>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string? Phone { get; init; }
}