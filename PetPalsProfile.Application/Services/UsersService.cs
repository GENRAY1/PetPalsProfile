using PetPalsProfile.Domain.Abstractions;
using PetPalsProfile.Domain.Models;

namespace PetPalsProfile.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.Get();
        }
        public async Task<Guid> CreateUser(User user)
        {
            return await _usersRepository.Create(user);
        }
        public async Task<Guid> UpdateUser(Guid id, string firstName, string lastName, string email, string password, string description, DateOnly birthdate)
        {
            return await _usersRepository.Update(id, firstName, lastName, email, password, description, birthdate);
        }
        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _usersRepository.Delete(id);
        }

    }
}
