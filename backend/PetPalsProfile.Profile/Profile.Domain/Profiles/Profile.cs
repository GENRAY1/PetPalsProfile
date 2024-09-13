using Profile.Domain.Abstractions;

namespace Profile.Domain.Profiles;

public class Profile : Entity
{
    public required string Username { get; set; }
    
    public required Guid AccountId { get; init; }
    public string? MainPhoto { get; set; }
    public string? Description { get; set; }
    
    public DateTime? DateOfBirth { get; set; }

    public List<ProfileContact> Contacts = new ();
}