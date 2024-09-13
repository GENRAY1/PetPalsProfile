using MediatR;
using Profile.Domain.Abstractions;
using Profile.Domain.Pets;
using Profile.Domain.Pets.PetTypes;
using Profile.Domain.Profiles;

namespace Profile.Application.Pets.Create;

public class CreatePetCommandHandler(
    IPetRepository petRepository,
    IPetTypeRepository petTypeRepository,
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork) 
    : IRequestHandler<CreatePetCommand, CreatePetDtoResponse>
{
    private const int MaxPetsPerProfile = 3;

    public async Task<CreatePetDtoResponse> Handle(CreatePetCommand request, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(request.ProfileId, cancellationToken);

        if (profile is null)
            throw new Exception("Profile not found");

        var profilePetsCount = await petRepository.GetCountForProfile(request.ProfileId, cancellationToken);

        if (profilePetsCount >= MaxPetsPerProfile)
            throw new Exception("Pet limit reached. The maximum number of pets is 3.");

        PetType? petType = null;

        if (request.TypeId.HasValue)
        {
            var type = await petTypeRepository.GetById(request.TypeId.Value, cancellationToken);

            petType = type ?? throw new Exception("Pet type not found");
        }

        var newPet = new Pet
        {
            Id = Guid.NewGuid(),
            DateOfBirth = request.DateOfBirth,
            Description = request.Description,
            Gender = request.Gender,
            MainPhoto = request.MainPhoto,
            Name = request.Name,
            ProfileId = request.ProfileId,
            Breed = request.Breed,
            TypeId = request.TypeId,
        };
        
        petRepository.Add(newPet);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreatePetDtoResponse
        {
            PetId = newPet.Id
        };
    }
}