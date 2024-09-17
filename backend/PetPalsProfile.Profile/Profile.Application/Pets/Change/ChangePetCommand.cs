using MediatR;
using Profile.Domain.Pets;
using Profile.Domain.Pets.Enums;

namespace Profile.Application.Pets.Update;

public class ChangePetCommand : IRequest
{
    public required Guid Id { get; init;}
    
    public string? Name { get; init; }
    
    public string? MainPhoto { get; init; }
    
    public string? Description  { get; init; }
    
    public DateTime? DateOfBirth { get; init; }
    
    public PetGenderEnum? Gender { get; init; }
    
    public Guid? TypeId { get; init; }
    
    public string? Breed { get; init; }
}