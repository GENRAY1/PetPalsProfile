using MediatR;

namespace Profile.Application.Pets.Get;

public class GetPetQuery : IRequest<GetPetDtoResponse>
{
    public required Guid PetId { get; init; }
}