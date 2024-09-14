namespace PetPalsProfile.Application.Account.GetLogged;

public class GetLoggedAccountResponse
{
    public required Guid Id { get; init; }
    public string? Phone { get; init; }
    public required string Email { get; init; }
    public required RoleResponse Role { get; init; }
}