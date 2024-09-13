using MediatR;
using Profile.Domain.Abstractions;
using Profile.Domain.Pets.PetTypes;

namespace Profile.Application.Pets.PetTypes.Delete;

public class DeletePetTypeCommandHandler(
    IPetTypeRepository petTypeRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePetTypeCommand>
{
    public async Task Handle(DeletePetTypeCommand request, CancellationToken cancellationToken)
    {
        var petType = await petTypeRepository.GetById(request.PetTypeId, cancellationToken);
        
        if(petType is null)
            throw new Exception("Pet type not found");
        
        petTypeRepository.Delete(petType);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}