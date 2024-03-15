using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsProfile.Domain.Models
{
    public class User
    {
        public const int MIN_PASSWORD_LENGTH = 6;
        public const int MAX_PASSWORD_LENGTH = 64;

        public const int MIN_LOGIN_LENGTH = 6;
        public const int MAX_LOGIN_LENGTH = 32;

        private User(Guid id, string firstName, string lastName, string email, string login, string password,
        string description, DateOnly birthdate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            Email = email;
            Description = description;
            Login = login;
            Password = password;

        }
        public Guid Id { get; }
        public string FirstName { get; } = string.Empty;
        public string LastName { get; } = string.Empty;
        public DateOnly Birthdate { get; }
        public string Email { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public string Login { get; } = string.Empty;
        public string Password { get; } = string.Empty;

        public static (User user, string message) Create(Guid id, string firstName, string lastName, string email, string login, string password, string description = "", DateOnly birthdate = new DateOnly())
        {
            //TODO Сделать валидацию других полей
            string message = String.Empty;

            var getErrorMessage = (string parameterName) => $"Параметр {parameterName} не может быть пустым ";

            if (string.IsNullOrEmpty(firstName))
            {
                message = getErrorMessage("firstName");
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                message = getErrorMessage(lastName);
            }
            else if (string.IsNullOrEmpty(email))
            {
                message = getErrorMessage(email);
            }
            else if (string.IsNullOrEmpty(login) || MAX_LOGIN_LENGTH > login.Length || password.Length > MIN_LOGIN_LENGTH)
            {
                message = getErrorMessage(login);
            }
            else if (string.IsNullOrEmpty(password) || MAX_PASSWORD_LENGTH > password.Length || password.Length > MIN_PASSWORD_LENGTH)
            {
                message = getErrorMessage(password);
            }
            else if (string.IsNullOrEmpty(email))
            {
                message = getErrorMessage(email);
            }

            User user = new User(id, firstName, lastName, email, login, password, description, birthdate); 
            return (user, message);
        }

    }
}
