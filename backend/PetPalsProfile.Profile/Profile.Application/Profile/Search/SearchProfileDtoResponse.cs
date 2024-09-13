using Profile.Application.Profile.Types;

namespace Profile.Application.Profile.Search;

public class SearchProfileDtoResponse
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required Guid AccountId { get; init; }
    public string? MainPhoto { get; init; }
    public string? Description { get; init; }
    public DateTime? DateOfBirth { get; init; }
}