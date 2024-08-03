namespace PetPalsProfile.Application.Account.GetLogged;

public class RoleResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Localization { get; init; }
}