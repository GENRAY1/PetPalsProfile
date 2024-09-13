using Profile.Domain.Profiles.Enums;

namespace Profile.Application.Profile.Types;

public class ProfileContactType
{
    public required ProfileContactTypeEnum ContactType { get; init; }
    
    public required string Link { get; init; }
}