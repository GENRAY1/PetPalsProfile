using MediatR;
using Profile.Application.Common;

namespace Profile.Application.Profile.Search;

public class SearchProfilesQuery : IRequest<ListDtoResponse<SearchProfileDtoResponse>>
{
    public required PaginationDtoRequest Pagination { get; init; }
}