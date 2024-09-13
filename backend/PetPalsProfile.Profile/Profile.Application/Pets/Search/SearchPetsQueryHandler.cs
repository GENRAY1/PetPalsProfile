using MediatR;
using Profile.Application.Common;
using Profile.Domain.Pets;

namespace Profile.Application.Pets.Search;

public class SearchPetsQueryHandler(IPetRepository petRepository)
    : IRequestHandler<SearchPetsQuery, ListDtoResponse<SearchPetDtoResponse>>
{
    public async Task<ListDtoResponse<SearchPetDtoResponse>> Handle(SearchPetsQuery request,
        CancellationToken cancellationToken)
    {
        var pets = await petRepository.Find(
            request.ProfileId,
            request.Pagination.Skip,
            request.Pagination.Take,
            cancellationToken
        );


        var count = await petRepository.GetCountForProfile(request.ProfileId, cancellationToken);
            
        var response = new ListDtoResponse<SearchPetDtoResponse>
        {
            TotalCount = count,
            List = pets.Select(
                pet => new SearchPetDtoResponse
                {
                    Id = pet.Id,
                    ProfileId = pet.ProfileId,
                    DateOfBirth = pet.DateOfBirth,
                    Name = pet.Name,
                    MainPhoto = pet.MainPhoto,
                    Description = pet.Description,
                    Type = pet.Type is not null
                        ? new IdNamePairDtoResponse
                        {
                            Id = pet.Type.Id,
                            Name = pet.Type.Name,
                        }
                        : null,
                    Breed = pet.Breed,
                    Gender = pet.Gender
                }
            ).ToList()
        };

        return response;
    }
}