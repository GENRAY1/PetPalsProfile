using Microsoft.EntityFrameworkCore;
using Profile.Domain.Pets.PetTypes;
using Profile.Infrastructure.Database;

namespace Profile.Infrastructure.Repositories;

public class PetTypeRepository(ApplicationDbContext context) 
    : IPetTypeRepository
{
    public async Task<PetType?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<PetType>()
            .AsNoTracking()
            .FirstOrDefaultAsync(type => type.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<PetType>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Set<PetType>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
    {
       return !await context.Set<PetType>()
           .AnyAsync(p => EF.Functions.ILike(p.Name, $"%{name}%"), cancellationToken);
    }

    public void Add(PetType petType)
    {
        context.Set<PetType>()
            .Add(petType);
        
    }

    public void Delete(PetType petType)
    {
        context.Set<PetType>()
            .Remove(petType);
    }
}