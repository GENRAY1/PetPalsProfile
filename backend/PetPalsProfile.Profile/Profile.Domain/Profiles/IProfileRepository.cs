using Profile.Domain.Pets;

namespace Profile.Domain.Profiles;

public interface IProfileRepository
{
    Task<Profile?> GetById(
        Guid id, 
        CancellationToken cancellationToken);

    Task<Profile?> GetByUsername(
        string username, 
        CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<Profile>> Find(
        int skip,
        int take,
        CancellationToken cancellationToken);
    
    void Add(Profile newProfile);
    
    void UpdateAsync(Profile entity);
    
    Task<int> Count(CancellationToken cancellationToken);
}