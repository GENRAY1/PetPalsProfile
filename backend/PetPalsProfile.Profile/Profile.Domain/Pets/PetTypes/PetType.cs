using Profile.Domain.Abstractions;

namespace Profile.Domain.Pets.PetTypes;

public class PetType : Entity
{
    public required string Name { get; init; }
}