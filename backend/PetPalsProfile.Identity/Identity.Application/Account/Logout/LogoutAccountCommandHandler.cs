using MediatR;
using PetPalsProfile.Application.Abstractions.Authentication;
using PetPalsProfile.Domain.Absractions;
using PetPalsProfile.Domain.Accounts;

namespace PetPalsProfile.Application.Account.Logout;

public class LogoutAccountCommandHandler(
    IJwtProvider jwtProvider,
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<LogoutAccountCommand>
{
    public async Task Handle(LogoutAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetById(request.AccountId, cancellationToken);
        
        if (account is null)
            throw new Exception($"Account not found");

        account.DisableRefreshToken();
        
        accountRepository.Update(account);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}