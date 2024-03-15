namespace PetPalsProfile.API.Contracts
{
    public record class UserPostRequest(string FirstName, string LastName, string Email, string Login, string Password);
    public record class UserUpdateRequest(string FirstName, string LastName, string Email, string Password);

}
