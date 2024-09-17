using Microsoft.EntityFrameworkCore;
using Profile.Domain.Profiles;
using Profile.Infrastructure.Database;

namespace Profile.Infrastructure.Repositories;

public class ProfileRepository(ApplicationDbContext context) : IProfileRepository
{
    public async Task<Domain.Profiles.Profile?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<Domain.Profiles.Profile>()
            .Include(p => p.Contacts)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Domain.Profiles.Profile?> GetByUsername(string username, CancellationToken cancellationToken)
    {
        return await context.Set<Domain.Profiles.Profile>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Username == username, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Domain.Profiles.Profile>> Find(int skip, int take, CancellationToken cancellationToken)
    {
        return await context.Set<Domain.Profiles.Profile>()
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public void Add(Domain.Profiles.Profile newProfile)
    {
        context.Set<Domain.Profiles.Profile>()
            .Add(newProfile);
    }

    public void UpdateAsync(Domain.Profiles.Profile profile)
    {
        context.Set<Domain.Profiles.Profile>()
            .Update(profile);
    }

    public async Task<int> Count(CancellationToken cancellationToken)
    {
        return await context.Set<Domain.Profiles.Profile>()
            .CountAsync(cancellationToken);
    }
}