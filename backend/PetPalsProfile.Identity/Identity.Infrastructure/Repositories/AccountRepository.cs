using Microsoft.EntityFrameworkCore;
using PetPalsProfile.Domain.Accounts;
using PetPalsProfile.Domain.UserAccounts;
using PetPalsProfile.Infrastructure.Database;

namespace PetPalsProfile.Infrastructure.Repositories;

public class AccountRepository(ApplicationDbContext context) 
    : IAccountRepository
{
    public async Task<Account?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await context
            .Set<Account>()
            .Include(account => account.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<Account?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context
            .Set<Account>()
            .Include(account => account.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Account?> GetByRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        return await context
            .Set<Account>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RefreshToken!.Value == refreshToken, cancellationToken);
    }

    public void Add(Account account)
    {
        context.Set<Account>()
            .Add(account);
    }

    public void Update(Account account)
    {
        context.Set<Account>()
            .Update(account);
    }
}