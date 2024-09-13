using MediatR;

namespace Profile.Application.Pets.PetTypes.Create;

public class CreatePetTypeCommand : IRequest<CreatePetTypeDtoResponse>
{
    public required string Name { get; init; }
}