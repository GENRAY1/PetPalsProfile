using MediatR;
using PetPalsProfile.Application.Abstractions.Authentication;
using PetPalsProfile.Application.Abstractions.PasswordManager;
using PetPalsProfile.Domain.Absractions;
using PetPalsProfile.Domain.Accounts;


namespace PetPalsProfile.Application.Account.Login;

public class LoginAccountCommandHandler(
    IPasswordManager passwordManager,
    IAccountRepository accountRepository,
    IJwtProvider jwtProvider,
    IUnitOfWork unitOfWork) 
    : IRequestHandler<LoginAccountCommand, LoginAccountResponse>
{
    public async Task<LoginAccountResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetByEmail(request.Email, cancellationToken);
        
        if (account is null)
            throw new Exception($"User with email {request.Email} not found");
        
        if (!passwordManager.Verify(request.Password, account.PasswordHash))
            throw new Exception("Incorrect password");
        
        var accessToken = jwtProvider.GenerateAccessToken(account);
        var refreshToken = jwtProvider.GenerateRefreshToken();
        var refreshTokenExpirationDate = DateTime.UtcNow
            .AddMinutes(jwtProvider.GetRefreshTokenLifetimeInMinutes());
        
        account.SetRefreshToken(new AccountRefreshToken
            {
                ExpirationDate = refreshTokenExpirationDate,
                Value = refreshToken,
                IsActive = true
            }
        );
        
        accountRepository.Update(account);

        await unitOfWork.SaveChangesAsync(cancellationToken);
            
        return new LoginAccountResponse
        {
            RefreshToken = refreshToken,
            AccessToken = accessToken
        };
    }
}