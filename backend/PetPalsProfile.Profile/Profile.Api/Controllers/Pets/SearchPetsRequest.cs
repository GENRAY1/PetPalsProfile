using Profile.Application.Common;

namespace Profile.Api.Controllers.Pets;

public class SearchPetsRequest
{
    public required Guid ProfileId { get; init; }
    
    public required PaginationDtoRequest Pagination { get; init; }
}