using MediatR;
using Profile.Domain.Abstractions;
using Profile.Domain.Pets;
using Profile.Domain.Pets.PetTypes;

namespace Profile.Application.Pets.Update;

public class ChangePetCommandHandler(
    IPetRepository petRepository,
    IPetTypeRepository petTypeRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<ChangePetCommand>
{
    public async Task Handle(ChangePetCommand request, CancellationToken cancellationToken)
    {
        var pet = await petRepository.GetById(request.Id, cancellationToken);

        if (pet is null)
            throw new Exception("Pet not found");

        if (request.MainPhoto is not null)
            pet.MainPhoto = request.MainPhoto;

        if (request.Name is not null)
            pet.Name = request.Name;
        
        if(request.Description is not null)
            pet.Description = request.Description;

        if (request.Breed is not null)
            pet.Breed = request.Breed;
        
        if (request.DateOfBirth.HasValue)
            pet.DateOfBirth = request.DateOfBirth.Value;

        if (request.Gender.HasValue)
            pet.Gender = request.Gender.Value;
        
        if (request.TypeId.HasValue)
        {
            var type = await petTypeRepository.GetById(request.TypeId.Value, cancellationToken);
            
            if (type is null)
                throw new Exception("Pet type not found");
            
            pet.Type = type;
        }
        
        petRepository.UpdateAsync(pet);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}