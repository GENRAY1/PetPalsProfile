using Profile.Application.Profile.Types;
using Profile.Domain.Profiles;

namespace Profile.Application.Profile.Get;

public class GetProfileDtoResponse
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required Guid AccountId { get; init; }
    public string? MainPhoto { get; init; }
    public string? Description { get; init; }
    public DateTime? DateOfBirth { get; init; }
    
    public long Subscribers { get; init; }
    public List<ProfileContactType>? Contacts { get; init; } 
}