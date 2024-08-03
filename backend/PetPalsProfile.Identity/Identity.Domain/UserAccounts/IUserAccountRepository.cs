namespace PetPalsProfile.Domain.UserAccounts;

public interface IUserAccountRepository
{
    Task <UserAccount?> GetByEmail(string email, CancellationToken cancellationToken);
    Task <UserAccount?> GetById(Guid id, CancellationToken cancellationToken);

    void Add(UserAccount userAccount);
}