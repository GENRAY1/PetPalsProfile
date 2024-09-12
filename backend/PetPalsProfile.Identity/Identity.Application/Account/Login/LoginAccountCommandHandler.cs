using MediatR;
using PetPalsProfile.Domain.Accounts;
using PetPalsProfile.Domain.UserAccounts;
using PetPalsProfile.Infrastructure.Authentication;
using PetPalsProfile.Infrastructure.PasswordManager;

namespace PetPalsProfile.Application.Account.Login;

public class LoginAccountCommandHandler(
    IPasswordManager passwordManager,
    IJwtProvider jwtProvider,
    IAccountRepository accountRepository)
    : IRequestHandler<LoginAccountCommand, LoginAccountResponse>
{
    public async Task<LoginAccountResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetByEmail(request.Email, cancellationToken);
        
        if (account is null)
            throw new Exception($"User with email {request.Email} not found");
        
        if (!passwordManager.Verify(request.Password, account.PasswordHash))
            throw new Exception("Incorrect password");

        var token = jwtProvider.GenerateToken(account);
        return new LoginAccountResponse
        {
            AccessToken = token
        };
    }
}