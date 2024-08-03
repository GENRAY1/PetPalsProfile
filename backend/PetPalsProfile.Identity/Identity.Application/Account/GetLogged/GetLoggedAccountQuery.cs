using MediatR;

namespace PetPalsProfile.Application.Account.GetLogged;

public class GetLoggedAccountQuery: IRequest<GetLoggedAccountResponse>
{
    public Guid AccountId { get; init; }
}