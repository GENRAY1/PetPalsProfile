using Microsoft.EntityFrameworkCore;
using Profile.Domain.Pets;
using Profile.Domain.Pets.PetTypes;
using Profile.Infrastructure.Database;

namespace Profile.Infrastructure.Repositories;

public class PetRepository(ApplicationDbContext context) : IPetRepository
{
    public async Task<Pet?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<Pet>()
            .Include(pet => pet.Type)
            .AsNoTracking()
            .FirstOrDefaultAsync(pet => pet.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Pet>> Find(
        Guid profileId, 
        int skip, 
        int take, 
        CancellationToken cancellationToken)
    {
       return await context.Set<Pet>()
            .Where(pet => pet.ProfileId == profileId)
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    

    public void Add(Pet newPet)
    {
         context.Set<Pet>()
             .Add(newPet);
    }

    public void UpdateAsync(Pet entity)
    {
        context.Set<Pet>()
            .Update(entity);
    }
    
    public async Task<int> GetCountForProfile(Guid profileId, CancellationToken cancellationToken)
    {
        return await context.Set<Pet>()
            .Where(pet => pet.ProfileId == profileId)
            .CountAsync(cancellationToken);
    }

    public async Task<PetType?> GetPetTypeById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<PetType>()
            .AsNoTracking()
            .FirstOrDefaultAsync(petType => petType.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<PetType>> GetPetTypes(CancellationToken cancellationToken)
    {
        return await context.Set<PetType>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}