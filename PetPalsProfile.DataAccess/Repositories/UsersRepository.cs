using PetPalsProfile.DataAccess.Entities;
using PetPalsProfile.Domain.Abstractions;
using PetPalsProfile.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetPalsProfile.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Get()
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .ToListAsync();
            var users = userEntity.Select(u => User.Create(u.Id, u.FirstName, u.LastName, u.Email, u.Login, u.Password, u.Description, u.Birthdate).user)
                .ToList();
            return users;
        }

        public async Task<UserEntity> Get(Guid  id)
        {
            UserEntity? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            else
            {
                return user;
            }
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                Birthdate = user.Birthdate,
                Description = user.Description,
            };
            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Users.Where(u => u.Id == id).
                ExecuteDeleteAsync();
            return id;
        }
        /*
        public async Task<Guid> Update(Guid id, string firstName, string lastName, string email, string password, string description, DateOnly birthdate)
        {
            await _context.Users.Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.FirstName, u => firstName)
                    .SetProperty(u => u.LastName, u => lastName)
                    .SetProperty(u => u.Email, u => email)
                    .SetProperty(u => u.Password, u => password)
                    .SetProperty(u => u.Description, u => description)
                    .SetProperty(u => u.Birthdate, u => birthdate));
            return id;
        }*/
        public async Task<Guid> Update(Guid id, string? firstName = null, string? lastName = null, string? email = null, string? password= null, string? description = null, DateOnly? birthdate =null)
        {

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                user.FirstName = firstName;
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                user.LastName = lastName;
            }

            if (!string.IsNullOrEmpty(email))
            {
                user.Email = email;
            }

            if (!string.IsNullOrEmpty(password))
            {
                user.Password = password;
            }

            if (!string.IsNullOrEmpty(description))
            {
                user.Description = description;
            }

            if (birthdate.HasValue)
            {
                user.Birthdate = birthdate.Value;
            }

            await _context.SaveChangesAsync();

            return user.Id;
        }

    }
}
