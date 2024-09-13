using Profile.Application.Profile.Types;

namespace Profile.Api.Controllers.Profiles;

public class CreateProfileRequest
{
    public required Guid AccountId { get; init; }
    public string? Username { get; init; }
    public string? MainPhoto { get; init; }
    public string? Description { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public List<ProfileContactType>? Contacts { get; init; }
}