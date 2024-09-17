using MediatR;
using PetPalsProfile.Application.Abstractions.Authentication;
using PetPalsProfile.Domain.Absractions;
using PetPalsProfile.Domain.Accounts;

namespace PetPalsProfile.Application.Account.RefreshToken;

public class RefreshTokenCommandHandler(
    IJwtProvider jwtProvider,
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Guid accountId = jwtProvider.GetUserIdFromToken(request.AccessToken);

        var account = await accountRepository.GetById(accountId, cancellationToken);
        
        if(account?.RefreshToken is null)
            throw new Exception("Refresh token not found. Login again");
        
        if(account.RefreshToken!.IsActive == false)
            throw new Exception("Refresh token not active. Login again");
        
        if(account.RefreshToken.ExpirationDate < DateTime.UtcNow)
            throw new Exception("Refresh token expired. Login again");
        
        if(account.RefreshToken.Value != request.RefreshToken)
            throw new Exception("Refresh token not valid");
        
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
        
        return new RefreshTokenResponse
        {
            RefreshToken = refreshToken,
            AccessToken = accessToken
        };
    }
}