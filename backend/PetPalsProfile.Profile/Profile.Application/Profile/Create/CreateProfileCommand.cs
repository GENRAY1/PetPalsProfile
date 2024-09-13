using MediatR;
using Profile.Application.Profile.Types;
using Profile.Domain.Profiles;

namespace Profile.Application.Profile.Create;

public class CreateProfileCommand : IRequest<CreateProfileDtoResponse>
{
    public required Guid AccountId { get; init; }
    public string? Username { get; init; }
    public string? MainPhoto { get; init; }
    public string? Description { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public List<ProfileContactType>? Contacts { get; init; }
}