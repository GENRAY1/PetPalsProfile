using MediatR;
using Profile.Application.Common;
using Profile.Domain.Pets;

namespace Profile.Application.Pets.Get;

public class GetPetQueryHandler(
    IPetRepository petRepository
    ) : IRequestHandler<GetPetQuery, GetPetDtoResponse>
{
    public async Task<GetPetDtoResponse> Handle(GetPetQuery request, CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetById(request.PetId, cancellationToken);

        if (pet is null)
            throw new Exception("Pet not found");

        return new GetPetDtoResponse
        {
            Id = pet.Id,
            ProfileId = pet.ProfileId,
            DateOfBirth = pet.DateOfBirth,
            Name = pet.Name,
            MainPhoto = pet.MainPhoto,
            Description = pet.Description,
            Type = pet.Type is not null
            ? new IdNamePairDtoResponse{
                Id = pet.Type.Id,
                Name = pet.Type.Name,
            } : null,
            Breed = pet.Breed,
            Gender = pet.Gender
        };
    }
}