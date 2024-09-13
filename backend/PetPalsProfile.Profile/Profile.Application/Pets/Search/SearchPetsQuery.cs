using MediatR;
using Profile.Application.Common;

namespace Profile.Application.Pets.Search;

public class SearchPetsQuery : IRequest<ListDtoResponse<SearchPetDtoResponse>>
{
    public required Guid ProfileId { get; init; }
    
    public required PaginationDtoRequest Pagination { get; init; }
}