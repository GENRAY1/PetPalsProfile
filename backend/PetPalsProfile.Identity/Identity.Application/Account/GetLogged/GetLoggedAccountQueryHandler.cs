using MediatR;
using PetPalsProfile.Domain.Accounts;
using PetPalsProfile.Domain.UserAccounts;

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
            Username = userAccount.Username,
            Email = userAccount.Email,
            Role = new RoleResponse()
            {
                Id = userAccount.Role.Id,
                Name = userAccount.Role.Name,
                Localization = userAccount.Role.Localization
            }
        };
    }
}