using Profile.Domain.Abstractions;
using Profile.Domain.Pets.Enums;
using Profile.Domain.Pets.PetTypes;

namespace Profile.Domain.Pets;

public class Pet : Entity
{
    public required Guid ProfileId { get; init; }
    
    public required string Name { get; set; }
    
    public string? MainPhoto { get; set; }
    
    public string? Description  { get; set; }
    
    public DateTime? DateOfBirth { get; set; }

    public PetType? Type { get; set; }
    
    public Guid? TypeId { get; set; }
    
    public string? Breed { get; set; }
    
    public PetGenderEnum? Gender { get; set; }
}