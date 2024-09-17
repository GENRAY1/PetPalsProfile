using MediatR;

namespace Profile.Application.Pets.PetTypes.Delete;

public class DeletePetTypeCommand : IRequest
{
    public required Guid PetTypeId { get; init; }
}