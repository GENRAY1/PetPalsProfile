using Profile.Domain.Pets.Enums;

namespace Profile.Api.Controllers.Pets;

public class CreatePetRequest
{
    public required Guid ProfileId { get; init; }
    
    public required string Name { get; init; }
    
    public string? MainPhoto { get; init; }
    
    public string? Description  { get; init; }
    
    public DateTime? DateOfBirth { get; init; }
    
    public Guid? TypeId { get; init; }
    
    public string? Breed { get; init; }
    
    public PetGenderEnum? Gender { get; set; }
}