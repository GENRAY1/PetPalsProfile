namespace Profile.Domain.Pets.PetTypes;

public interface IPetTypeRepository
{
    Task<PetType?> GetById( 
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<PetType>> GetAll(
        CancellationToken cancellationToken);
    
    Task<bool> IsNameUnique (
        string name,
        CancellationToken cancellationToken);
    
    void Add(PetType petType);

    void Delete(PetType petType);
}