using MediatR;
using Profile.Application.Common;
using Profile.Domain.Pets.PetTypes;

namespace Profile.Application.Pets.PetTypes.GetAll;

public class GetPetTypesQueryHandler (
    IPetTypeRepository petTypeRepository)
    : IRequestHandler<GetPetTypesQuery, IReadOnlyCollection<IdNamePairDtoResponse>>
{
    public async Task<IReadOnlyCollection<IdNamePairDtoResponse>> Handle(GetPetTypesQuery request, CancellationToken cancellationToken)
    {
        var petTypes = await petTypeRepository.GetAll(cancellationToken);
        
        var response = petTypes
            .Select(type => new IdNamePairDtoResponse
            {
                Id = type.Id,
                Name = type.Name
            })
            .ToList();
        
        return response;
    }
}