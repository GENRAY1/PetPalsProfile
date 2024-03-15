using PetPalsProfile.Domain.Models;

namespace PetPalsProfile.Domain.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> Create(User user);
        Task<Guid> Delete(Guid id);
        Task<List<User>> Get();
        Task<Guid> Update(Guid id, string? firstName = null, string? lastName = null, string? email = null, string? password = null, string? description = null, DateOnly? birthdate = null);
    }
}