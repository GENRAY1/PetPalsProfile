using Profile.Domain.Pets.PetTypes;

namespace Profile.Domain.Pets;

public interface IPetRepository
{
    Task<Pet?> GetById(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Pet>> Find(
        Guid profileId,
        int skip,
        int take,
        CancellationToken cancellationToken);

    void Add(Pet newPet);

    void UpdateAsync(Pet entity);

    Task<int> GetCountForProfile(
        Guid profileId,
        CancellationToken cancellationToken);
    
}