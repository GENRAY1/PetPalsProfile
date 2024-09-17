using Profile.Application.Common;
using Profile.Domain.Pets;
using Profile.Domain.Pets.Enums;

namespace Profile.Application.Pets.Get;

public class GetPetDtoResponse
{
    public required Guid Id { get; init;}
    
    public required Guid ProfileId { get; init; }
    
    public required string Name { get; init; }
    
    public string? MainPhoto { get; init; }
    
    public string? Description  { get; init; }
    
    public DateTime? DateOfBirth { get; init; }
    
    public IdNamePairDtoResponse? Type { get; init; }
    
    public string? Breed { get; init; }
    
    public PetGenderEnum? Gender { get; init; }
}