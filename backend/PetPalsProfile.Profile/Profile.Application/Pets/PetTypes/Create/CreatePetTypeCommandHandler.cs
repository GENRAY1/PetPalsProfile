using MediatR;
using Profile.Domain.Abstractions;
using Profile.Domain.Pets.PetTypes;

namespace Profile.Application.Pets.PetTypes.Create;

public class CreatePetTypeCommandHandler(
    IPetTypeRepository petTypeRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePetTypeCommand, CreatePetTypeDtoResponse>
{
    public async Task<CreatePetTypeDtoResponse> Handle(CreatePetTypeCommand request, CancellationToken cancellationToken)
    {
        bool isNameUnique = await petTypeRepository.IsNameUnique(request.Name, cancellationToken);
        
        if(!isNameUnique)
            throw new Exception("Pet type name already exists");
        
        var petType = new PetType
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        
        petTypeRepository.Add(petType);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CreatePetTypeDtoResponse
        {
            PetTypeId = petType.Id
        };
    }
}