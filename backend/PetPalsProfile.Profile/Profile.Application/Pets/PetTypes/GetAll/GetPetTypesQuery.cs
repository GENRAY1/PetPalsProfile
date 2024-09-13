using MediatR;
using Profile.Application.Common;

namespace Profile.Application.Pets.PetTypes.GetAll;

public class GetPetTypesQuery : IRequest<IReadOnlyCollection<IdNamePairDtoResponse>> { } 