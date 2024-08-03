using PetPalsProfile.Domain.Absractions;
using PetPalsProfile.Infrastructure.Database;

namespace PetPalsProfile.Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
       await context.SaveChangesAsync(cancellationToken);
}