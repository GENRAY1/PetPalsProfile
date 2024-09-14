using MediatR;
using Microsoft.Extensions.Logging;
using PetPalsProfile.Domain.Absractions;
using PetPalsProfile.Domain.Accounts;
using PetPalsProfile.Domain.UserAccounts;
using PetPalsProfile.Infrastructure.PasswordManager;

namespace PetPalsProfile.Application.Account.Register;

public class RegisterAccountCommandHandler(
    IPasswordManager passwordManager,
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork,
    ILogger<RegisterAccountCommandHandler> logger)
    :IRequestHandler<RegisterAccountCommand>
{
    public async Task Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = passwordManager.Generate(request.Password);

        var account = await accountRepository.GetByEmail(request.Email, cancellationToken);
        
        if (account is not null)
            throw new Exception($"User with email {request.Email} already exists");
        
        var newAccount = Domain.Accounts.Account.Create(
            email: request.Email,
            passwordHash: passwordHash,
            phone:request.Phone);
        
        accountRepository.Add(newAccount);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}