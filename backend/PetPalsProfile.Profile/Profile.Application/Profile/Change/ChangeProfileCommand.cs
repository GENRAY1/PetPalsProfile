using MediatR;
using Profile.Application.Profile.Types;

namespace Profile.Application.Profile.Change;

public class ChangeProfileCommand : IRequest
{
    public required Guid Id { get; init; }
    public string? Username { get; init; }
    public string? MainPhoto { get; init; }
    public string? Description { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public List<ProfileContactType>? Contacts { get; init; }
}