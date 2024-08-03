using Microsoft.EntityFrameworkCore;
using PetPalsProfile.Domain.UserAccounts;
using PetPalsProfile.Infrastructure.Database;

namespace PetPalsProfile.Infrastructure.Repositories;

public class UserAccountRepository(ApplicationDbContext context) 
    : IUserAccountRepository
{
    public async Task<UserAccount?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await context
            .Set<UserAccount>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<UserAccount?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context
            .Set<UserAccount>()
            .Include(account => account.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Add(UserAccount userAccount)
    {
        context
            .Set<UserAccount>()
            .Add(userAccount);
    }
}