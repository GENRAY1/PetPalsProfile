using MediatR;

namespace PetPalsProfile.Application.Account.Logout;

public class LogoutAccountCommand : IRequest
{
    public required Guid AccountId { get; init; }
}