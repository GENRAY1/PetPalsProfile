namespace Profile.Application.Common;

public class PaginationDtoRequest
{
    public int Skip { get; init; }
    
    public int Take { get; init; }
}