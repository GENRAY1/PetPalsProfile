using Profile.Application.Common;

namespace Profile.Api.Controllers.Profiles;

public class SearchProfileRequest
{
    public required PaginationDtoRequest Pagination { get; init; }
}