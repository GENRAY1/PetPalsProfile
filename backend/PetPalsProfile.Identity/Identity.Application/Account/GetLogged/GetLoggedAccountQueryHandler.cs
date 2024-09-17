using MediatR;
using PetPalsProfile.Domain.Accounts;

namespace PetPalsProfile.Application.Account.GetLogged;

public class GetLoggedAccountQueryHandler(
    IAccountRepository accountRepository)
    :IRequestHandler<GetLoggedAccountQuery, GetLoggedAccountResponse>
{
    public async Task<GetLoggedAccountResponse> Handle(GetLoggedAccountQuery request, CancellationToken cancellationToken)
    {
        var userAccount = await accountRepository
            .GetById(request.AccountId, cancellationToken);
        
        if(userAccount is null)
            throw new Exception($"Account with id {request.AccountId} not found ");

        return new GetLoggedAccountResponse()
        {
            Id = userAccount.Id,
            Phone = userAccount.Phone,
            Email = userAccount.Email,
            Role = userAccount.Roles.Select(role => new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Localization = role.Localization
            }).ToList()
        };
    }
}