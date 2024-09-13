using Profile.Domain.Abstractions;
using Profile.Domain.Profiles.Enums;

namespace Profile.Domain.Profiles;

public class ProfileContact
{
    public required Guid ProfileId { get; init; }
    
    public required ProfileContactTypeEnum ContactType { get; init; }
    
    public required string Link { get; set; }
}